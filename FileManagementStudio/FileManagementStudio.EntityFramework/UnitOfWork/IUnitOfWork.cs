using FileManagementStudio.EntityFramework.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.EntityFramework.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFileEntityRepository FileEntities { get; }
        IFolderRepository Folders { get; }
        IUserRepository Users { get; }
        public Task Save();
        protected void Dispose(bool disposing);
        public void Dispose();

    }
}
