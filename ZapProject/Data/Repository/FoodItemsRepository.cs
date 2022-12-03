using Microsoft.EntityFrameworkCore;
using ZapProject.Data.Interfaces;
using ZapProject.Models;

namespace ZapProject.Data.Repository
{
    public class FoodItemsRepository : IFoodItems
    {
        private readonly ApplicationDbContext _context;
        public FoodItemsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(FoodItem item)
        {
            _context.Add(item);
            return Save();
        }

        public bool Delete(FoodItem item)
        {
            _context.Remove(item);
            return Save();
        }   

        public async Task<IEnumerable<FoodItem>> GetAll() => await _context.FoodItems.Include(i => i.Category).ToListAsync();

        public async Task<FoodItem> GetByIdAsync(int id) => await _context.FoodItems.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);

        public async Task<FoodItem> GetByIdAsyncNoTracking(int id) => await _context.FoodItems.Include(i => i.Category).AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true: false;
        }

        public bool Update(FoodItem item)
        {
            _context.Update(item);
            return Save();
        }
    }
}
