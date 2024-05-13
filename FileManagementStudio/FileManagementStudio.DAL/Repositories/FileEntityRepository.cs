using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories.Interfaces;

namespace FileManagementStudio.DAL.Repositories
{
    public class FileEntityRepository : GenericRepository<FileEntity>, IFileEntityRepository
    {
        public FileEntityRepository(FileManagementStudioDbContext context) : base(context)
        {
        }
    }
}
