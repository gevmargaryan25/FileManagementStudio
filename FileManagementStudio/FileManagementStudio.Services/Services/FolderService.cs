using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Services.Services.Interfaces;

namespace FileManagementStudio.Services.Services
{
    public class FolderService : IFolderService<Folder>
    {
        private readonly IFolderRepository _repository;
        public FolderService(IFolderRepository repository)
        {
            _repository = repository;
        }
        public Task Add(Folder entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<Folder> folders)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Folder entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Folder>> GetEntities()
        {
            throw new NotImplementedException();
        }

        public Task<Folder> GetEntity()
        {
            throw new NotImplementedException();
        }

        public Task Remove(Folder entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<Folder> folders)
        {
            throw new NotImplementedException();
        }

        public Task Update(Folder entity)
        {
            throw new NotImplementedException();
        }
    }
}
