using System.ComponentModel.DataAnnotations.Schema;

namespace ZapProject.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }

       	[ForeignKey("FoodItem")]
		public int ItemId { get; set; }
        public virtual FoodItem? Item { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public virtual Order? Order { get; set; }
    }
}
