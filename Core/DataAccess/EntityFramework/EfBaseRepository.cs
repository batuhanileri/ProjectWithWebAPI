using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework
{
    public class EfBaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext ,new()

    {
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using TContext context = new();
            await context.Set<TEntity>().AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using TContext context = new();
            var deleted = await context.Set<TEntity>().FindAsync(id);
            context.Set<TEntity>().Remove(deleted);
            var data = await context.SaveChangesAsync();
            if (data > 0)
                return true;
            return false;
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            using TContext context = new();
            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using TContext context = new();
            return filter == null ? await context.Set<TEntity>().ToListAsync()
                                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using TContext context = new();
            context.Set<TEntity>().Update(entity);
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
