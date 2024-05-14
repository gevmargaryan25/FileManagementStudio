using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Server.DTOs;
using FileManagementStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var files = await _fileService.GetFilesByUser(email);
            var list = files.Select(x =>
            {
                return new FileDTO()
                {
                    Name = x.Name,
                    Type = x.FileType,
                    Size = x.FileSize
                };
            }).ToList();
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var email = User.Claims.ToList()[0].Value.ToString();
            BlobResponseDto? response = await _storage.UploadAsync(file, email);

            if (response.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                try
                {
                    await _fileService.AddAsync(file, email);
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
            var email = User.Claims.ToList()[0].Value.ToString();
            filename = string.Concat(email, filename);
            BlobDto? file = await _storage.DownloadAsync(filename);
            if (file == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"File {filename} could not be downloaded.");
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, file);
            }
        }

        [HttpDelete("{filename}")]
        public async Task<IActionResult> Delete(string filename)
        {
            var email = User.Claims.ToList()[0].Value.ToString();
            string blobName = string.Concat(email, filename);
            BlobResponseDto response = await _storage.DeleteAsync(blobName);
            if (response.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                try
                {
                    await _fileService.RemoveAsync(filename, email);
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

        [HttpPost(nameof(Share))]
        public async Task<IActionResult> Share([FromBody]ShareDto shareDto)
        {
            var originEmail = User.Claims.ToList()[0].Value.ToString();
            var blob = await _storage.DownloadAsync(string.Concat(originEmail, shareDto.FileName));
            blob.Name = shareDto.FileName;
            var response = await _storage.UploadStreamAsync(blob, shareDto.Email);
            if (response.Error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            await _fileService.ShareFile(originEmail, shareDto.Email, shareDto.FileName);
            return StatusCode(StatusCodes.Status200OK, response.Status);
        }
    }
}

