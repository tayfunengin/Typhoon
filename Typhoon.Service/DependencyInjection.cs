using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Typhoon.Domain.Validation;
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

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<CategoryValidaton>();
            services.AddValidatorsFromAssemblyContaining<ProductValidaton>();
            return services;
        }
    }
}
