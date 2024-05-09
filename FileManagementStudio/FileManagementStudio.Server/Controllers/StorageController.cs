using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Server.DTOs;
using FileManagementStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        public StorageController(IAzureStorage storage, IFileService<FileEntity> fileService)
        {
            _storage = storage;
            _fileService = fileService;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
            //implement a conberter from fileentity to blobdto
            List<BlobDto>? files = await _storage.ListAsync();

            return StatusCode(StatusCodes.Status200OK, files);
        }

        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string userName = User.Identity.Name;
            BlobResponseDto? response = await _storage.UploadAsync(file, userName);

            if (response.Error == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "The File upload was not succeeded");
                }
                try
                {
                    await _fileService.AddAsync(file, userId);
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
            string blobName = User.Identity.Name + filename;
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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

