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

        public async Task<AppUser?> GetUserById(string id) => await _context.Users.Include(u => u.Address).Where(u => u.Id == id).FirstOrDefaultAsync();

        public async Task<AppUser> GetByIdNoTracking(string id) => await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();

        public int AddNewUserAddress(Address address)
        {
            _context.Add(address);
            if(Save()) return address.Id;
            return -1;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Update(user);
            return Save();
        }

        public bool Update(Address address)
        {
            _context.Update(address);
            return Save();
        }
    }
}
