using FileManagementStudio.DAL.Entities;
using FileManagementStudio.DAL.Repositories.Interfaces;
using FileManagementStudio.Services.Services.Interfaces;
using System.Linq.Expressions;

namespace FileManagementStudio.Services.Services
{
    public class FIleService : IFileService<FileEntity>
    {
       
        public Task Add(FileEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRange(IEnumerable<FileEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileEntity>> GetEntities(Expression<Func<Folder, bool>> filter, Func<IQueryable<Folder>, IOrderedQueryable<Folder>> orderBy, string includeProperties)
        {
            throw new NotImplementedException();
        }

        public Task<FileEntity> GetEntityById(object id)
        {
            throw new NotImplementedException();
        }

        public void Remove(FileEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(object id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveRange(IEnumerable<FileEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Update(FileEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
