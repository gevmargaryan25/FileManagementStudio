using FileManagementStudio.Server.Entities;
using FileManagementStudio.Server.Repositories.Interfaces;

namespace FileManagementStudio.Server.Repositories
{
    public class FolderRepository : GenericRepository<Folder>, IFolderRepository
    {
        public FolderRepository(FileManagementStudioDbContext context) : base(context)
        {
        }
    }
}
