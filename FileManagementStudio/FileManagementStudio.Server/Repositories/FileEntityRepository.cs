using FileManagementStudio.Server.Entities;
using FileManagementStudio.Server.Repositories.Interfaces;

namespace FileManagementStudio.Server.Repositories
{
    public class FileEntityRepository : GenericRepository<FileEntity>, IFileEntityRepository
    {
        public FileEntityRepository(FileManagementStudioDbContext context) : base(context)
        {
        }
    }
}
