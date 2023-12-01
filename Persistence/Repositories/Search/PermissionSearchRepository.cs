using Domain.Entities;
using Domain.Generics.Interfaces;
using Microsoft.Extensions.Configuration;
using Nest;

namespace Persistence.Repositories.Search
{
    public class PermissionSearchRepository : ElasticSearchRepository<Permission>, ISearchPermissionRepository
    {
        private readonly IElasticClient _elasticClient;
        private readonly string _indexName;

        public PermissionSearchRepository(IElasticClient client, IConfiguration configuration) : base(client, configuration)
        {
            _elasticClient = client;
            _indexName = "permissions";
        }
    }
}
