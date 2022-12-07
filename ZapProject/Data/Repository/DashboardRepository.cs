using Microsoft.EntityFrameworkCore;
using ZapProject.Data.Interfaces;
using ZapProject.Models;
using System.Collections.Generic;

namespace ZapProject.Data.Repository
{
    public class DashboardRepository : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<FoodItem>> GetAllItems() => await _context.FoodItems.Include(i => i.Category).ToListAsync();

        public async Task<List<FoodItem>> GetUserItems()
        {
            var curUser = _httpContextAccessor.HttpContext.User.GetUserId();
            var items = _context.FavoriteItems.Include(i => i.Item).Where(i => i.UserId == curUser).Select(i => i.Item).ToList();
            return items.ToList();
        }
    }
}
