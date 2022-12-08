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
        private readonly IFavItems _favItemsRepository;
        public DashboardRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IFavItems favItemsRepository)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _favItemsRepository = favItemsRepository;
        }
        public async Task<List<FoodItem>> GetAllItems() => await _context.FoodItems.Include(i => i.Category).ToListAsync();

        public async Task<List<FoodItem>> GetUserItems()
        {
            var items = await _favItemsRepository.GetItems();
            return items.ToList();
        }
    }
}
