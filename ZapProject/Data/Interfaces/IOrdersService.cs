using ZapProject.Models;

namespace ZapProject.Data.Interfaces
{
    public interface IOrdersService
    {
        Task<List<Order>> GetAllUsersOrders();
		Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserId(string userId);
    }
}
