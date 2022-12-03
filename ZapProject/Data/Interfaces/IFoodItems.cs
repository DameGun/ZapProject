using ZapProject.Models;

namespace ZapProject.Data.Interfaces
{
    public interface IFoodItems
    {
        Task<IEnumerable<FoodItem>> GetAll();

        Task<FoodItem> GetByIdAsync(int id);

        Task<FoodItem> GetByIdAsyncNoTracking(int id);

        bool Add(FoodItem item);

        bool Update(FoodItem item);

        bool Delete(FoodItem item);

        bool Save();
    }
}
