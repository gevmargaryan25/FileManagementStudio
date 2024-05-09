using FileManagementStudio.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.Services.Services.Interfaces
{
    public interface IGeneralService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetEntities(Expression<Func<Folder, bool>> filter,
           Func<IQueryable<Folder>, IOrderedQueryable<Folder>> orderBy,
           string includeProperties);
        Task<TEntity> GetEntityById(object id);
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(object id);
        Task RemoveRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
    }
}
