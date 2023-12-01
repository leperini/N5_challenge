using AutoMapper.Internal;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using N5_challenge.Behaviours;
using System.Reflection;
using FluentValidation;


namespace N5_challenge.Extensions
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddMediatRExtensions(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }

        private static IServiceCollection AddAutoMapperClasses(IServiceCollection services, Action<IServiceProvider, IMapperConfigurationExpression> configAction,
            IEnumerable<Assembly> assembliesToScan, ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        {
            if (configAction != null)
            {
                services.AddOptions<MapperConfigurationExpression>().Configure<IServiceProvider>((options, sp) => configAction(sp, options));
            }
            if (assembliesToScan != null)
            {
                assembliesToScan = new HashSet<Assembly>(assembliesToScan.Where(a => !a.IsDynamic && a != typeof(Mapper).Assembly));
                services.Configure<MapperConfigurationExpression>(options => options.AddMaps(assembliesToScan));
                var openTypes = new[]
                {
                    typeof(IValueResolver<,,>),
                    typeof(IMemberValueResolver<,,,>),
                    typeof(ITypeConverter<,>),
                    typeof(IValueConverter<,>),
                    typeof(IMappingAction<,>)
                };
                foreach (var type in assembliesToScan.SelectMany(a => a.GetTypes().Where(type => type.IsClass && !type.IsAbstract && Array.Exists(openTypes,
                    openType => type.GetGenericInterface(openType) != null))))
                {
                    // use try add to avoid double-registration
                    services.TryAddTransient(type);
                }
            }
            // Just return if we've already added AutoMapper to avoid double-registration
            if (services.Any(sd => sd.ServiceType == typeof(IMapper)))
            {
                return services;
            }
            services.AddSingleton<AutoMapper.IConfigurationProvider>(sp =>
            {
                // A mapper configuration is required
                var options = sp.GetRequiredService<IOptions<MapperConfigurationExpression>>();
                return new MapperConfiguration(options.Value);
            });
            services.Add(new ServiceDescriptor(typeof(IMapper), sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService), serviceLifetime));
            return services;
        }
    }
}
