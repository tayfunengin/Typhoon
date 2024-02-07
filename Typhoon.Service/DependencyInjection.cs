using Microsoft.Extensions.DependencyInjection;
using Typhoon.Service.Services;
using Typhoon.Service.Services.Interfaces;

namespace Typhoon.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IService<,,,,>), typeof(Service<,,,,>));
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
