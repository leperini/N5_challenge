using Infraestructure.Kafka.Consumer;
using Infraestructure.Kafka.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class ServicesRegistration
    {

        public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(sp => new KafkaProducerWrapper<string, string>(configuration["Kafka:BootstrapServers"]));
            services.AddSingleton(sp => new KafkaConsumerWrapper<string, string>(
                    configuration["Kafka:BootstrapServers"],
                    configuration["Kafka:ConsumerGroupId"],
                    configuration["Kafka:ConsumerTopic"])
            );

            return services;
        }
    }
}
