using Microsoft.AspNetCore.Mvc;
using ZapProject.Data;

namespace ZapProject.ViewComponents
{
	public class ShoppingCartSummary : ViewComponent
	{
		private readonly ShoppingCart _shoppingCart;
		public ShoppingCartSummary(ShoppingCart shoppingCart)
		{
			_shoppingCart = shoppingCart;
		}

		public IViewComponentResult Invoke()
		{
			var items = _shoppingCart.GetShoppingCartItems().Result;

			return View(items.Count);
		}
	}
}
