using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZapProject.Models;

namespace ZapProject.ViewModels
{
    public class FoodItemsViewModel
    {
        public IEnumerable<FoodItem> AllItems { get; set; }

        public string? currCategory { get; set; }
    }
}
