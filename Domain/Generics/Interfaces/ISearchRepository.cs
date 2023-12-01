namespace Domain.Generics.Interfaces
{
    public interface ISearchRepository<T> : IGenericRepository<T> where T : class
    {
        Task<List<T>> SearchAsync(string term);
    }
}
