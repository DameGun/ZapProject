using Microsoft.AspNetCore.Mvc;
using ZapProject.Data.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ZapProject.ViewModels;
using ZapProject.Data;
using ZapProject.Models;

namespace ZapProject.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IFoodItems _foodItems;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IFoodItems foodItems, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _foodItems = foodItems;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            List<Order> orders = new List<Order>();
            if(User.IsInRole("admin"))
            {
				orders = await _ordersService.GetAllUsersOrders();
			}
            else
            {
				string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				orders = await _ordersService.GetOrdersByUserId(userId);
			}
            
            return View(orders);
        }

        public async Task<IActionResult> ShoppingCart()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems =  items;

            var shoppingCartVM = new ShoppingCartViewModel()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartVM);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _foodItems.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _foodItems.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = await _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

    }
}
