using FileManagementStudio.Server.Repositories.Interfaces;

namespace FileManagementStudio.Server.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FileManagementStudioDbContext _context;

        //how all repositories will refer to the same context according to me we must create by new f. e. FileEntitie=new Fileentities(_context)
        public UnitOfWork(FileManagementStudioDbContext context, IFileEntityRepository fileEntities,
            IFolderRepository folders, IUserRepository users)
        {
            _context = context;

            FileEntities = fileEntities;

            Folders = folders;

            Users = users;
        }

        public UnitOfWork()
        {

        }

        public IFileEntityRepository FileEntities { get; private set; }
        public IFolderRepository Folders { get; private set; }
        public IUserRepository Users { get; private set; }


        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void IUnitOfWork.Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}
