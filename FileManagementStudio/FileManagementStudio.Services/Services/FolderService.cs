using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Services.Services.Interfaces;
using System.Linq.Expressions;

namespace FileManagementStudio.Services.Services
{
    public class FolderService : IFolderService<Folder>
    {
        private readonly IFolderRepository _repository;
        public FolderService(IFolderRepository repository)
        {
            _repository = repository;
        }
        public async Task Add(Folder entity)
        {
            await _repository.Add(entity);
        }

        public async Task AddRange(IEnumerable<Folder> folders)
        {
            await _repository.AddRange(folders);
        }

        public Task<IEnumerable<Folder>> GetEntities(Expression<Func<Folder, bool>> filter = null,
           Func<IQueryable<Folder>, IOrderedQueryable<Folder>> orderBy = null,
           string includeProperties = "")
        {
            return _repository.Get(filter, orderBy, includeProperties);
        }

        public Task<Folder> GetEntityById(object id)
        {
            return _repository.GetByID(id);
        }

        public void Remove(Folder entity)
        {
            _repository.Remove(entity);
        }
        public void Remove(object id)
        {
            _repository.Remove(id);
        }

        public async Task RemoveRange(IEnumerable<Folder> folders)
        {
             _repository.RemoveRange(folders);
        }

        public void Update(Folder entity)
        {
            _repository.Update(entity);
        }
    }
}
