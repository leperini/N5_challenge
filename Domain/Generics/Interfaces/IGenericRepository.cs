using Domain.Models;
using System.Linq.Expressions;

namespace Domain.Generics.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        #region Sync Methods

        IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            params string[] includeProperties
        );

        T Get(int id) => default(T);

        void Add(T entity) { }

        void Remove(int id) { }

        void Remove(T entity) { }

        #endregion


        #region Async Methods
        Task<List<T>> GetAllAsync(
            Dictionary<string, int> additionalProps,
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            PaginatorModel pagination,
            params Expression<Func<T, object>>[] includes
            );

        Task<T> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<List<T>> AddMassiveAsync(List<T> entities) => default;

        Task<T> UpdateAsync(int id, T entity);

        Task<T> RemoveAsync(int id);


        #endregion


    }
}
