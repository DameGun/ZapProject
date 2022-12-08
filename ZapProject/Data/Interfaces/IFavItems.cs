using ZapProject.Models;

namespace ZapProject.Data.Interfaces
{
	public interface IFavItems
	{
		Task<List<FoodItem>> GetItems();
		Task<FavouriteItem> GetItemById(int id);
		bool Add(int id);
		bool Delete(FavouriteItem item);
		bool Save();
	}
}
