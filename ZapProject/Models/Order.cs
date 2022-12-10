using System.ComponentModel.DataAnnotations.Schema;

namespace ZapProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Email { get; set; }

        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
