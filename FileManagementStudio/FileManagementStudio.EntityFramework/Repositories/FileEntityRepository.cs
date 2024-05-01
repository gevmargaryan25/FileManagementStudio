using FileManagementStudio.EntityFramework.Context;
using FileManagementStudio.EntityFramework.Entities;
using FileManagementStudio.EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.EntityFramework.Repositories
{
    public class FileEntityRepository : GenericRepository<FileEntity>, IFileEntityRepository
    {
        public FileEntityRepository(FileManagementStudioDbContext context) : base(context)
        {
        }
    }
}
