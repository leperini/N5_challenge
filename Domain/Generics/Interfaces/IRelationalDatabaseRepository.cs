using System.Linq.Expressions;

namespace Domain.Generics.Interfaces
{
    public interface IRelationalDatabaseRepository<T> : IGenericRepository<T> where T : class
    {

        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter,
            params string[] includeProperties
            );

        Task<T> GetFirstOrDefaultAsync(
            Expression<Func<T, bool>> filter,
            params string[] includeProperties
            );

        Task<int> CountAsync(Expression<Func<T, bool>> filter);


    }
}
