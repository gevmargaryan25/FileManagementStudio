
using FileManagementStudio.Server.DTOs;

namespace FileManagementStudio.DAL.Repositories.Interfaces
{
    public interface IAzureStorage
    {
        Task<BlobResponseDto> UploadAsync(IFormFile file, string userName);
        Task<BlobResponseDto> UploadStreamAsync(BlobDto blob, string userName);

        Task<BlobDto> DownloadAsync(string blobFilename);

        Task<BlobResponseDto> DeleteAsync(string blobFilename);

        Task<List<BlobDto>> ListAsync();
    }
}

