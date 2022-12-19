using Microsoft.AspNetCore.Mvc;
using ZapProject.Data.Interfaces;
using ZapProject.Models;
using ZapProject.ViewModels;

namespace ZapProject.Controllers
{
	public class DashboardController : Controller
	{
		private readonly IDashboardService _dashboardService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public DashboardController(IDashboardService dashboardService, IHttpContextAccessor httpContextAccessor) {
			_dashboardService = dashboardService;
			_httpContextAccessor = httpContextAccessor;
		}

		public void MapUserEdit(AppUser user, EditUserProfileViewModel editVM)
		{
			user.Id = editVM.Id;
			user.PhoneNumber = editVM.PhoneNumber;
			if (user.AddressId == null)
			{
				int addressId = _dashboardService.AddNewUserAddress(editVM.Address);
				if (addressId != 1) user.AddressId = addressId;
			}
			_dashboardService.Update(editVM.Address);
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

		[HttpGet]
		public async Task<IActionResult> EditUserProfile()
		{
			var curUserId = _httpContextAccessor.HttpContext.User.GetUserId();
			var user = await _dashboardService.GetUserById(curUserId);
			if (user == null) return View("Error");
			var editVM = new EditUserProfileViewModel()
			{
				Id = curUserId,
				PhoneNumber = user.PhoneNumber,
				AddressId = user.AddressId,
                Address = user.Address
            };
			return View(editVM);
		}

		[HttpPost]
		public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editVM)
		{
			if (!ModelState.IsValid)
			{
				ModelState.AddModelError("", "Ошибка редактирования профиля");
				return RedirectToAction("EditUserProfile", editVM);
			}
			
			AppUser user = await _dashboardService.GetByIdNoTracking(editVM.Id);

			MapUserEdit(user, editVM);

			_dashboardService.Update(user);
			return RedirectToAction("EditUserProfile");
		}
	}
}
