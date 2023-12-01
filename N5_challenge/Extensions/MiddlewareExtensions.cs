using N5_challenge.Middlewares;

namespace N5_challenge.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

        public static void UseApiLoggerMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiLoggerMiddleware>();
        }

        public static void UseKafkaIntegrationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<KafkaManagementMiddleware>();
        }

        public static IServiceCollection AddRestApiServices(this IServiceCollection services)
        {
            services.AddScoped<ErrorHandlerMiddleware>();
            services.AddScoped<ApiLoggerMiddleware>();
            services.AddScoped<KafkaManagementMiddleware>();

            return services;

            
        }

    }
}
