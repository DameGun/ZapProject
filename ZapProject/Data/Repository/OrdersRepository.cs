using Microsoft.EntityFrameworkCore;
using ZapProject.Data.Interfaces;
using ZapProject.Models;

namespace ZapProject.Data.Repository
{
    public class OrdersRepository : IOrdersService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFoodItems _itemsRepository;

        public OrdersRepository(ApplicationDbContext context, IFoodItems itemsRepository)
        {
            _context = context;
            _itemsRepository = itemsRepository;
        }

        public async Task<List<Order>> GetAllUsersOrders() => await _context.Orders
			.Include(o => o.OrderItems)
			.ThenInclude(o => o.Item)
			.Include(o => o.User)
			.ToListAsync();

		public async Task<List<Order>> GetOrdersByUserId(string userId) => await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(o => o.Item)
            .Include(o => o.User)
            .Where(n => n.UserId == userId)
            .ToListAsync();

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ItemId = item.FoodItem.Id,
                    OrderId = order.Id,
                    Price = item.FoodItem.Price
                };
                await _context.OrderItems.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }
    }
}
