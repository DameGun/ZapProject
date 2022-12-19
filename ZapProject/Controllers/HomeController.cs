using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZapProject.Data.Interfaces;
using ZapProject.Models;
using ZapProject.ViewModels;

namespace ZapProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeService _homeService;

        public HomeController(ILogger<HomeController> logger, IHomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _homeService.GetPopularItems();
            var homeVM = new HomeViewModel();
            if(items.Count != 0)
            {
                homeVM.Items = items;
            }
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}