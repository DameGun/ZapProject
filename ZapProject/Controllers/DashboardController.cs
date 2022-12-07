using Microsoft.AspNetCore.Mvc;
using ZapProject.Data.Interfaces;
using ZapProject.Models;
using ZapProject.ViewModels;

namespace ZapProject.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardService _dashboardService;
		public DashboardController(IDashboardService dashboardService) {
			_dashboardService = dashboardService;
		}
		public async Task<IActionResult> Index()
		{
			List<FoodItem> userItems = new List<FoodItem>();
			if (User.IsInRole("admin"))
			{
				userItems = await _dashboardService.GetAllItems();
			}
			else
			{
				userItems = await _dashboardService.GetUserItems();
			}
			
			var dashboardVM = new DashboardViewModel
			{
				UserItems = userItems,
			};
			return View(dashboardVM);
		}
	}
}
