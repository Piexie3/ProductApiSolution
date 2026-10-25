using Ecommerce.SharedLirary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;

namespace ProductApi.Infrastructure.DependencyInjection
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructreService(this IServiceCollection services, IConfiguration config)
        {
            // Register your infrastructure services here
            // Example: services.AddScoped<IProductRepository, ProductRepository>();
            //Add database connectivity
            // add authentication sceme

            SharedServiceContainer.AddSharedServices<ProductDbContext>(services, config, config["MySerilog:FileName"]!);

            // create dependency injection(DI)
            services.AddScoped<IProduct, ProductRepo>();

            return services;
        }
        public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder app)
        {
            // Add any middleware or policies specific to the infrastructure layer here
            // Example: app.UseMiddleware<YourCustomMiddleware>();


            // Registering middleware for handling exceptions and logging them
            // listen to only api gateway request
            SharedServiceContainer.UserSharedPloicies(app);
            return app;
        }
    }
}
