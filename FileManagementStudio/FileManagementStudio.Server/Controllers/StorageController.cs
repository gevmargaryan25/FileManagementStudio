using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Server.DTOs;
using Microsoft.AspNetCore.Mvc;
namespace FileManagementStudio.Server.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorage _storage;

        public StorageController(IAzureStorage storage)
        {
            _storage = storage;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
           List<BlobDto>? files = await _storage.ListAsync();

           return StatusCode(StatusCodes.Status200OK, files);
        }

        [HttpPost(nameof(Upload))]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            BlobResponseDto? response = await _storage.UploadAsync(file);

            if (response.Error == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
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
            BlobResponseDto response = await _storage.DeleteAsync(filename);

            if (response.Error == true)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, response.Status);
            }
        }
    }
}

