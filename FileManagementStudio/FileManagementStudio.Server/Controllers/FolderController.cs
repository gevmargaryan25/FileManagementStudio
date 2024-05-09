using FileManagementStudio.DAL.Entities;
using FileManagementStudio.Services.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FileManagementStudio.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolderController : ControllerBase
    {
        private readonly IFolderService<Folder> _folderService;

        public FolderController(IFolderService<Folder> folderService)
        {
            _folderService = folderService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllFolders([FromQuery] string filter = "", string orderBy = "")
        //{
        //    try
        //    {

        //        IEnumerable<Folder> folders = await _folderService.GetEntities(where _folderService.);
        //        return Ok(folders);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Folder folder)
        {
            try
            {
                await _folderService.Add(folder);
                return Ok("Folder created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Folder folderToDelete = await _folderService.GetEntityById(id);
                if (folderToDelete == null)
                {
                    return NotFound();
                }

                _folderService.Remove(folderToDelete);
                return Ok("Folder deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Implement other action methods for updating and deleting folders as needed
    }
}

