using Microsoft.Extensions.DependencyInjection;
using Typhoon.Core.Repositories;
using Typhoon.Respository.Repositories;

namespace Typhoon.Respository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
