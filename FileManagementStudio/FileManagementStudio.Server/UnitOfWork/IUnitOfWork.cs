using FileManagementStudio.Server.Repositories.Interfaces;

namespace FileManagementStudio.Server.UnitOfWork
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
