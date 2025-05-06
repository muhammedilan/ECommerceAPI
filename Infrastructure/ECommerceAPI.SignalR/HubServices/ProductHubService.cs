using ECommerceAPI.Application.Abstractions.Hubs;
using ECommerceAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace ECommerceAPI.SignalR.HubServices
{
    public class ProductHubService(IHubContext<ProductHub> _hubContext) : IProductHubService
    {
        private readonly IHubContext<ProductHub> _hubContext = _hubContext;

        public async Task ProductAddedMessageAsync(string message)
        {
            await _hubContext.Clients.All.SendAsync("receiveProductAddedMessage", message);
        }
    }
}
