using Microsoft.EntityFrameworkCore;
using ZapProject.Data.Interfaces;
using ZapProject.Models;

namespace ZapProject.Data.Repository
{
    public class DashboardRepository : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly IFavItems _favItemsRepository;
        public DashboardRepository(ApplicationDbContext context, IFavItems favItemsRepository)
        {
            _context = context;
            _favItemsRepository = favItemsRepository;
        }
        public async Task<List<FoodItem>> GetAllItems() => await _context.FoodItems.Include(i => i.Category).ToListAsync();

        public async Task<List<FoodItem>> GetUserItems() => await _favItemsRepository.GetItems();
    }
}
