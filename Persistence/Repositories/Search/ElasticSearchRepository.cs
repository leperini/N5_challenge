using Domain.Generics.Interfaces;
using Domain.Models;
using Microsoft.Extensions.Configuration;
using Nest;
using System.Linq.Expressions;

namespace Persistence.Repositories.Search
{
    public class ElasticSearchRepository<T> : ISearchRepository<T> where T : class
    {

        private readonly IElasticClient _client;
        private readonly string _indexName;


        public ElasticSearchRepository(IElasticClient client, IConfiguration configuration)
        {
            _client = client;
            _indexName = configuration["Elasticsearch:DefaultIndex"];
        }

        public async Task<T> AddAsync(T entity)
        {
            var response = await _client.IndexAsync(entity, i => i.Index(_indexName));
            return null;
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, params string[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync(Dictionary<string, int> additionalProps, Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, PaginatorModel pagination, params Expression<Func<T, object>>[] includes)
        {
            var response = await _client.SearchAsync<T>(s => s.Index(_indexName));
            return response.Documents
                .ToList();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var response = await _client.GetAsync<T>(id, g => g.Index(_indexName));
            return response.Source;
        }

        public async Task<T> RemoveAsync(int id)
        {
            var deleteItem = await GetByIdAsync(id);
            await _client.DeleteAsync<T>(id, d => d.Index(_indexName));
            return deleteItem;
        }

        public async Task<List<T>> SearchAsync(string term)
        {
            var response = await _client.SearchAsync<T>(s => s.Index(_indexName));
            return response.Documents
                .ToList();
        }

        public async Task<T> UpdateAsync(int id, T entity)
        {
            var response = await _client.UpdateAsync<T>(id, u => u.Index(_indexName).Doc(entity));
            return await GetByIdAsync(id);
        }
    }
}
