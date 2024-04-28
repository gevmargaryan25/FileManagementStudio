using System.Linq.Expressions;

namespace FileManagementStudio.Server.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        public Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        public Task<TEntity> GetByID(object id);

        public Task Add(TEntity entity);

        Task AddRange(IEnumerable<TEntity> entities);

        public Task Remove(object id);

        public void Remove(TEntity entityToDelete);

        void RemoveRange(IEnumerable<TEntity> entities);

        public void Update(TEntity entityToUpdate);
        public Task SaveFromRepository();

    }
}
