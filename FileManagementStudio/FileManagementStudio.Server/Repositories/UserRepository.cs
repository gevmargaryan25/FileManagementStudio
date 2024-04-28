using FileManagementStudio.Server.Entities;
using FileManagementStudio.Server.Repositories.Interfaces;

namespace FileManagementStudio.Server.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FileManagementStudioDbContext context) : base(context)
        {
        }
    }
}
