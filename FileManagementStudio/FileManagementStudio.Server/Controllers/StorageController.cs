using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Server.DTOs;
using FileManagementStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace FileManagementStudio.Server.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorage _storage;
        private readonly IFileService<FileEntity> _fileService;
        private readonly UserManager<User> _userManager;

        public StorageController(IAzureStorage storage, IFileService<FileEntity> fileService, UserManager<User> userManager)
        {
            _storage = storage;
            _fileService = fileService;
            _userManager = userManager;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
            var email = User.Claims.ToList()[0].Value.ToString();
            var users = _userManager.Users.ToList();
            var userId1 = users.FirstOrDefault(user => user.NormalizedEmail == email.ToUpper()).Id;
            var files = await _fileService.GetEntitiesAsync();
            var list = files.Where(x => x.UserId == userId1).ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var email = User.Claims.ToList()[0].Value.ToString();
            var users = _userManager.Users.ToList();
            var userId1 = users.FirstOrDefault(user => user.NormalizedEmail == email.ToUpper()).Id;

            BlobResponseDto? response = await _storage.UploadAsync(file, email);
            

            if (response.Error == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                if (userId1 == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "The File upload was not succeeded");
                }
                try
                {
                    await _fileService.AddAsync(file, userId1);
                }
                catch(ArgumentNullException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Incorrect file");
                }
                catch (RepositoryException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "The File upload was not succeeded");
                }
                catch (EntityNotFoundException ex)
                {
                    // must we react in another way
                    return StatusCode(StatusCodes.Status500InternalServerError, "The File upload was not succeeded");
                }
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet("{filename}")]
        public async Task<IActionResult> Download(string filename)
        {
            BlobDto? file = await _storage.DownloadAsync(filename);

            if (file == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
            }
            else
            {
                return File(file.Content, file.ContentType, file.Name);
            }
        }

        [HttpDelete("filename")]
        public async Task<IActionResult> Delete(string filename)
        {
            var email = User.Claims.ToList()[0].Value.ToString();
            var users = _userManager.Users.ToList();
            var userId = users.FirstOrDefault(user => user.NormalizedEmail == email.ToUpper()).Id;
            string blobName = string.Concat(email, filename);
            BlobResponseDto response = await _storage.DeleteAsync(blobName);

            if (response.Error == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                try
                {
                    await _fileService.RemoveAsync(filename, userId);
                }
                catch(EntityNotFoundException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Your action is failed");
                }
                catch(Exception ex)
                {
                    //ToDo
                }
                return StatusCode(StatusCodes.Status200OK, response.Status);
            }
        }
    }
}

