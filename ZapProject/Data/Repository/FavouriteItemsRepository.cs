using ZapProject.Data.Interfaces;
using ZapProject.Models;

namespace ZapProject.Data.Repository
{
	public class FavouriteItemsRepository : IFavItems
	{
		private readonly ApplicationDbContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IFoodItems _itemsRepository;

		public FavouriteItemsRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IFoodItems itemsRepository)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
			_itemsRepository = itemsRepository;
		}

		public bool Add(int id)
		{
			var userSession = _httpContextAccessor.HttpContext.User.GetUserId();
			var curUser = _context.Users.FirstOrDefault(u => u.Id == userSession);
			var item = _itemsRepository.GetByIdAsync(id);
			if (curUser == null && item == null) return false;
			var fav = new FavouriteItem
			{
				UserId = curUser.Id,
				ItemId = id
			};
			_context.Add(fav);
			return Save();
		}

		public bool Delete(FavouriteItem item)
		{
			_context.Remove(item);
			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
