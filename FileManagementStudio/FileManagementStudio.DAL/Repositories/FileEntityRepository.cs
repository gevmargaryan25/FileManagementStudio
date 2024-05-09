using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.DAL.Repositories
{
    public class FileEntityRepository : GenericRepository<FileEntity>, IFileEntityRepository
    {
        public FileEntityRepository(FileManagementStudioDbContext context) : base(context)
        {
        }
        public async Task<FileEntity> GetByFileNameAsync(string fileName, string userId)
        {
            try
            {
                return await context.Files
                    .Include(f=>f.User)
                      .ThenInclude(u=>u.Files)
                 .FirstOrDefaultAsync(f => f.UserId.Equals(userId) && f.Name.Equals(fileName)) ?? null;
            }
            catch(Exception ex)
            {
                throw new EntityNotFoundException("File for remove was nto found", ex);
            }
        }
    }
}
