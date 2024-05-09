using FileManagementStudio.DAL.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.Services.Services.Interfaces
{
    public interface IGeneralService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetEntitiesAsync();
        Task<TEntity> GetEntityAsync();
        Task AddAsync(IFormFile file, string userId);
        Task AddRangeAsync(IEnumerable<IFormFile> entities);
        Task RemoveAsync(string entityName, string userId);
        Task Remove(object id);
        Task RemoveRange(IEnumerable<TEntity> entities);
        Task Update(IFormFile entity);
    }
}
