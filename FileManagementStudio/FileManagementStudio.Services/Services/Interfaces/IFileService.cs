using FileManagementStudio.DAL.Entities;

namespace FileManagementStudio.Services.Services.Interfaces
{
    public interface IFileService<TEntity> : IGeneralService<TEntity> where TEntity : class
    {
        Task ShareFile(string originEmail, string destEmail, string fileName);
        Task<IEnumerable<FileEntity>> GetFilesByUser(string userEmail);
    }
}
