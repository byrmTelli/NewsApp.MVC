using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, new() 
        where TContext : DbContext, new()
    {

        #region AsyncMethods

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> fiter)
        {
            using(var context = new TContext())
            {
                return await context.Set<TEntity>().FirstOrDefaultAsync(fiter);
            }
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? await context.Set<TEntity>().ToListAsync()
                    : await context.Set<TEntity>().Where(filter).ToListAsync();
            } 
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State = EntityState.Added;
                await context.SaveChangesAsync();
                return entity;
            }
        }
        public async Task<List<TEntity>> AddAllAsync(List<TEntity> entityList)
        {
            using (var context = new TContext())
            {
                foreach(var entity in entityList)
                {
                    var addEntity = context.Entry(entity);
                    addEntity.State = EntityState.Added;
                }
                await context.SaveChangesAsync();
                return entityList;
            }
        }

        public async Task DeleteAsync(TEntity entity)
        {
            using (var context =new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State = EntityState.Deleted;

                await context.SaveChangesAsync();

            }
        }

        public async Task DeleteAllAsync(List<TEntity> entityList)
        {
            using (var context = new TContext())
            {
                foreach(var entity in entityList)
                {
                    var deleteEntity = context.Entry(entity);
                    deleteEntity.State = EntityState.Deleted;
                }
                await context.SaveChangesAsync();
            }
        }


        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updateEntity = context.Entry(entity);
                updateEntity.State = EntityState.Modified;
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<List<TEntity>> UpdateAllAsync(List<TEntity> entityList)
        {
            using (var context = new TContext())
            {
                foreach(var entity in entityList)
                {
                    var updateEntity = context.Entry(entity);
                    updateEntity.State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
                return entityList;
            }
        }

        #endregion


        #region SyncMethods
        public TEntity Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var addEntity = context.Entry(entity);
                addEntity.State= EntityState.Added;
                context.SaveChanges();
                return entity;
            }
        }
        public void Delete(TEntity entity)
        {
            using(var context = new TContext())
            {
                var deleteEntity = context.Entry(entity);
                deleteEntity.State= EntityState.Deleted;
                context.SaveChanges();
            }
        }
        public bool Exist(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>().Where(filter);
                if(includes != null && includes.Length > 0)
                {
                    foreach(var includeObject in includes)
                    {
                        query = query.Include(includeObject);
                    }
                }
                return query.Any();
            }
        }
        public IQueryable<TEntity> FindByCodition(Expression<Func<TEntity, bool>> expression)
        {
            var context = new TContext();
            return context.Set<TEntity>()
                .Where(expression).AsNoTracking();
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }
        public IQueryable<TEntity> GetAll_I(Expression<Func<TEntity, bool>> filter = null)
        {
            var context = new TContext();

            return filter == null
                ? context.Set<TEntity>().AsNoTracking()
                : context.Set<TEntity>().AsNoTracking().Where(filter);
        }
        public IQueryable<TEntity> GetAll_I_Params(Expression<Func<TEntity, bool>> filter = null, string[] includes = null)
        {
            var context = new TContext();
            if(includes != null)
            {
                var query = context.Set<TEntity>().AsNoTracking().AsQueryable();
                foreach(var item in includes)
                {
                    query = query.Include(item);
                }
                return filter == null
                    ? query
                    : query.Where(filter);
            }
            else
            {
                return filter == null
                    ? context.Set<TEntity>()
                    : context.Set<TEntity>().Where(filter);
            }
        }
        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }
        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] includes)
        {
            var context = new TContext();
            IQueryable<TEntity> query = context.Set<TEntity>();

            if(includes != null && includes.Length > 0)
            {
                foreach(var includeObject in includes)
                {
                    query = query.Include(includeObject);
                }
            }
            return query.AsNoTracking().ToList();
        }
        public TEntity Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
                return entity;
            }
        }
        #endregion




    }
}
