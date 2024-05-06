
using FileManagementStudio.Server.DTOs;

namespace FileManagementStudio.DAL.Repositories.Interfaces
{
    public interface IAzureStorage
    {
        Task<BlobResponseDto> UploadAsync(IFormFile file);

        Task<BlobDto> DownloadAsync(string blobFilename);

        Task<BlobResponseDto> DeleteAsync(string blobFilename);

        Task<List<BlobDto>> ListAsync();
    }
}

