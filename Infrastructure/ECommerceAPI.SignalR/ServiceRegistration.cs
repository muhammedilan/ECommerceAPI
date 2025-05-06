using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.HubServices;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerceAPI.SignalR
{
    public static class ServiceRegistration
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddScoped<IProductHubService, ProductHubService>();
            services.AddSignalR();
        }
    }
}
