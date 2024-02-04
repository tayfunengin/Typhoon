using Microsoft.Extensions.DependencyInjection;

namespace Typhoon.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IService<,,,,>), typeof(Service<,,,,>));
        }
    }
}
