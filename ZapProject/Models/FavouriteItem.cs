using System.ComponentModel.DataAnnotations.Schema;

namespace ZapProject.Models
{
	public class FavouriteItem
	{
		public int Id { get; set; }

		[ForeignKey("AppUser")]
		public string UserId { get; set; }
		public virtual AppUser? User { get; set; }

		[ForeignKey("FoodItem")]
		public int ItemId { get; set; }
		public virtual FoodItem? Item { get; set; }
	}
}
