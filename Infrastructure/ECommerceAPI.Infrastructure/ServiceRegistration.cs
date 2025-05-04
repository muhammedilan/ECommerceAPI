using ECommerceAPI.Application.Abstractions.Storage;
using ECommerceAPI.Infrastructure.Enums;
using ECommerceAPI.Infrastructure.Services.Storage;
using ECommerceAPI.Infrastructure.Services.Storage.Local;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }
        public static void AddStorage<T>(this IServiceCollection services) where T : class, IStorage
        {
            services.AddScoped<IStorage, T>();
        }
        public static void AddStorage(this IServiceCollection services, StorageType storageType)
        {
            switch (storageType)
            {
                case StorageType.Local:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
                case StorageType.Azure:

                    break;
                default:
                    services.AddScoped<IStorage, LocalStorage>();
                    break;
            }
        }
    }
}
