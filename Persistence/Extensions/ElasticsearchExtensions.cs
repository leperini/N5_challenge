using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace Persistence.Extensions
{
    public static class ElasticsearchExtensions
    {
        public static void AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {

            var options = configuration.GetSection("Elasticsearch").Get<ElasticSearchOptions>();
            //.Bind(options);

            var settings = new ConnectionSettings(new Uri(options.ConnectionString))
                .DefaultIndex(options.DefaultIndex);

            var client = new ElasticClient(settings);
            services.AddSingleton<IElasticClient>(client);
        }

    }
}
