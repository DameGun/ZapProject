using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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

		public async Task<List<FoodItem?>> GetItems() {
            var userSession = _httpContextAccessor.HttpContext.User.GetUserId();
            return await _context.FavoriteItems.Include(i => i.Item).Where(i => i.UserId == userSession).Select(i => i.Item).ToListAsync();
        }

		public async Task<FavouriteItem?> GetItemById(int id) {
            var userSession = _httpContextAccessor.HttpContext.User.GetUserId();
			return await _context.FavoriteItems.FirstOrDefaultAsync(i => i.UserId == userSession && i.ItemId == id);
        } 

		public bool Add(int id)
		{
            var userSession = _httpContextAccessor.HttpContext.User.GetUserId();
            var item = _itemsRepository.GetByIdAsync(id);
			var fav = new FavouriteItem
			{
				UserId = userSession,
				ItemId = id
			};
			_context.Add(fav);
			return Save();
		}

		public bool Delete(FavouriteItem favItem)
		{
			_context.Remove(favItem);
			return Save();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
