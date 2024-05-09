using FileManagementStudio.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.DAL.Repositories.Interfaces
{
    public interface IFileEntityRepository : IGenericRepository<FileEntity>
    {
        public Task<FileEntity> GetByFileNameAsync(string fileName, string userId);
    }
}
