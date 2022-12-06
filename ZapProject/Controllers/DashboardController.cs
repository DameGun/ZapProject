using Microsoft.AspNetCore.Mvc;

namespace ZapProject.Controllers
{
	public class DashboardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
