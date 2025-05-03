using ECommerceAPI.Application.Abstractions;
using ECommerceAPI.Persistence.Concretes;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ECommerceAPIDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
            services.AddSingleton<IProductService, ProductService>();
        }
    }
}
