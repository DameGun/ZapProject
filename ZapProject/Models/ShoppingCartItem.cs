namespace ZapProject.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public FoodItem FoodItem { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
