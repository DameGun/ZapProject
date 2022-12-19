using ZapProject.Models;

namespace ZapProject.Data.Interfaces
{
	public interface IHomeService
	{
		Task<List<FoodItem>> GetPopularItems();
	}
}
