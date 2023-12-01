using Domain.Generics;
using Domain.Generics.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;
using static Domain.Helpers.EntitiesHelper;

namespace Persistence.Repositories
{
    public class RelationalDatabaseRepository<T> : IRelationalDatabaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext context;
        internal DbSet<T> dbSet; // optional

        public RelationalDatabaseRepository(DbContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {

            dbSet.Add(entity);
            //dbSet.FindAsync()
        }

        public async Task<T> AddAsync(T entity)
        {
            EntityEntry<T> entityCreated = await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entityCreated.Entity;
        }

        public async Task<List<T>> AddMassiveAsync(List<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
            await context.SaveChangesAsync();
            return entities;
        }



        public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;
            if (!IsNull(filter))
            {
                query = query.Where(filter);
            }
            return await query.CountAsync();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params string[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            includeProperties.ToList()
                .ForEach(prop => query = query.Include(prop));

            // aplicate filter
            if (!IsNull(filter))
            {
                query = query.Where(filter);
            }


            // aplicate order by
            if (!IsNull(orderBy))
            {
                return orderBy(query).ToList();
            }
            return query.ToList();
        }

        public async Task<List<T>> GetAllAsync(Dictionary<string, int> additionalProps, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, PaginatorModel pagination = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            includes.ToList()
                .ForEach(prop => query = query.Include(prop));

            if (pagination != null)
            {
                int pageNumber = pagination.PageNumber, pageSize = pagination.PageSize;
                query = query.Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);
            }
            // aplicate filter
            if (!IsNull(filter))
            {
                query = query.Where(filter);
            }

            // aplicate order by
            if (!IsNull(orderBy))
            {
                query = orderBy(query);
            }

            List<T> resultsItems = await query.ToListAsync();
            additionalProps[KEY_TOTAL_COUNT] = resultsItems.Count();

            return resultsItems;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, params string[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            // observation: include properties : ToLower()
            // include properties will be comma separated
            includeProperties.ToList()
                .ForEach(prop => query = query.Include(prop));


            // aplicate filter
            if (!IsNull(filter))
            {
                query = query.Where(filter);
            }

            return query.FirstOrDefault();
        }

        public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params string[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            // include properties will be comma separated
            // observation: include properties : ToLower()
            // include properties will be comma separated
            includeProperties.ToList()
                .ForEach(prop => query = query.Include(prop));

            // aplicate filter
            if (!IsNull(filter))
            {
                query = query.Where(filter);
            }


            return await query.FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            T entityRemove = dbSet.Find(id);

            Remove(entityRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<T> RemoveAsync(int id)
        {
            T entityRemove = dbSet.Find(id);
            entityRemove.IsDeleted = 1;
            context.Entry(entityRemove).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entityRemove;
            //dbSet.
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {

            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entity;
        }
    }
}
