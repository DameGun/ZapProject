using ZapProject.Models;

namespace ZapProject.Data.Interfaces
{
    public interface IDashboardService
    {
        Task<List<FoodItem>> GetAllItems();
        Task<List<FoodItem>> GetUserItems();
        Task<AppUser?> GetUserById(string id);
        Task<AppUser> GetByIdNoTracking(string id);
        bool Save();
        bool Update(AppUser user);
        bool Update(Address address);
        int AddNewUserAddress(Address address);
    }
}
