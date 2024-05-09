using FileManagementStudio.DAL.Context;
using FileManagementStudio.DAL.Exceptions;
using FileManagementStudio.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FileManagementStudio.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        internal FileManagementStudioDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(FileManagementStudioDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                IQueryable<TEntity> query = dbSet;

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> GetByID(object id)
        {
            var result = await dbSet.FindAsync(id);
            if (result is null)
            {
                throw new EntityNotFoundException();
            }
            return result;
        }

        public virtual async Task Add(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity); 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.StackTrace);
            }
        }

        public virtual async Task AddRange(IEnumerable<TEntity> entities)
        {
            try { await dbSet.AddRangeAsync(entities); }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual async void Remove(object id)
        {
            try
            {
                TEntity entityToDelete = await dbSet.FindAsync(id);
                Remove(entityToDelete);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void Remove(TEntity entityToDelete)
        {
            try
            {
                if (context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public virtual async Task SaveFromRepository()
        {
            try { await context.SaveChangesAsync(); }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message);
            }
        }
    }
}
