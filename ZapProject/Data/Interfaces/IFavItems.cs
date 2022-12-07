using ZapProject.Models;

namespace ZapProject.Data.Interfaces
{
	public interface IFavItems
	{
		bool Add(int id);
		bool Delete(FavouriteItem item);
		bool Save();
	}
}
