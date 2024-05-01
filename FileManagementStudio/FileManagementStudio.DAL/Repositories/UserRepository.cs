using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(FileManagementStudioDbContext context) : base(context)
        {
        }
    }
}
