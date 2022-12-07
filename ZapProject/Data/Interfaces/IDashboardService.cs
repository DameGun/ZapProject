using ZapProject.Models;

namespace ZapProject.Data.Interfaces
{
    public interface IDashboardService
    {
        Task<List<FoodItem>> GetAllItems();
        Task<List<FoodItem>> GetUserItems();
    }
}
