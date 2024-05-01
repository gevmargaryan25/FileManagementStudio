using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Services.Services.Interfaces;

namespace FileManagementStudio.Services.Services
{
    public class FIleService : IFileService<FileEntity>
    {
        private readonly IFileEntityRepository _repository;
        public FIleService(IFileEntityRepository repository)
        {
            _repository = repository;
        }
        public Task Add(FileEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<FileEntity> files)
        {
            throw new NotImplementedException();
        }

        public Task Delete(FileEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileEntity>> GetEntities()
        {
            throw new NotImplementedException();
        }

        public Task<FileEntity> GetEntity()
        {
            throw new NotImplementedException();
        }

        public Task Remove(FileEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<FileEntity> files)
        {
            throw new NotImplementedException();
        }

        public Task Update(FileEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
