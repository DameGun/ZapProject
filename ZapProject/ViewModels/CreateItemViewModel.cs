using ZapProject.Data.Enum;

namespace ZapProject.ViewModels
{
    public class CreateItemViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public double Weight { get; set; }

        public string Vendor { get; set; }

        public string Ingredients { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public bool Avaliable { get; set; }

        public string Img { get; set; }
    }
}
