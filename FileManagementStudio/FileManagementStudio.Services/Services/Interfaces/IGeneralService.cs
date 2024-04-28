using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.Services.Services.Interfaces
{
    internal interface IGeneralService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetEntities();
        Task<TEntity> GetEntity();
        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task Delete(TEntity entity);
        Task Remove(TEntity entity);
        Task Remove(object id);
        Task RemoveRange(IEnumerable<TEntity> entities);
        Task Update(TEntity entity);
    }
}
