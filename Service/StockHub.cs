using Microsoft.AspNetCore.SignalR;

namespace ECommerceAPI
{
    public class StockHub : Hub
    {
        public async Task UpdateStock(string ProductId, int newStockLevel)
        {
            await Clients.All.SendAsync("ReceiveStockUpdate", ProductId, newStockLevel);
        }
    }
}
