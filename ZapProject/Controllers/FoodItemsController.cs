using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ZapProject.Data;
using ZapProject.Data.Enum;
using ZapProject.Data.Interfaces;
using ZapProject.Models;
using ZapProject.ViewModels;

namespace ZapProject.Controllers
{
    public class FoodItemsController : Controller
    {
        private readonly IFoodItems _itemsRepository;
        private readonly ApplicationDbContext _categories;
        private readonly IFavItems _favItems;
        public FoodItemsController(IFoodItems itemsRepository, ApplicationDbContext categories, IFavItems favItems) {
            _itemsRepository = itemsRepository;
            _categories = categories;
            _favItems = favItems;
        }

		[HttpGet]
		public async Task<IActionResult> Index(string? category)
        {
            string currCategory = string.Empty;
            IEnumerable<FoodItem> items;

            if(category.IsNullOrEmpty())
            {
                items = await _itemsRepository.GetAll();
            }
            else
            {
                currCategory = category;
                items = await _itemsRepository.GetAll();
                items = items.Where(i => i.Category.ToString() == currCategory).ToList();
            }

            var itemObj = new FoodItemsViewModel
            {
                AllItems = items,
                currCategory = currCategory
            };

            return View(itemObj);
        }

		[HttpGet]
		public async Task<IActionResult> Details(int id)
        {
            FoodItem foodItem = await _itemsRepository.GetByIdAsync(id);
			bool favCheck = false;
			if (User.Identity.IsAuthenticated)
            {
				var favItem = await _favItems.GetItemById(id);
				if (favItem != null) favCheck = true;
			}

            var detailsVM = new DetailsViewModel
            {
                Item = foodItem,
                IsFavourite = favCheck,
            };

            ViewData["Title"] = foodItem.Name;
            return View(detailsVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_categories.Category, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateItemViewModel foodItem)
        {
            if (ModelState.IsValid)
            {
                return View(foodItem);
            }
            var item = new FoodItem
            {
                Name = foodItem.Name,
                Description = foodItem.Description,
                Price = foodItem.Price,
                Weight = foodItem.Weight,
                Vendor = foodItem.Vendor,
                Ingredients = foodItem.Ingredients,
                CategoryId = foodItem.CategoryId,
                Avaliable = foodItem.Avaliable,
                Img = foodItem.Img
            };

            _itemsRepository.Add(item);
            return RedirectToAction("Index");
        }

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
        {
            var item = await _itemsRepository.GetByIdAsync(id);
            if (item == null) return View("Error");
            ViewBag.Categories = new SelectList(_categories.Category, "Id", "Name");
            var itemVM = new EditItemViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                Weight = item.Weight,
                Vendor = item.Vendor,
                Ingredients = item.Ingredients,
                CategoryId = item.CategoryId,
                Avaliable = item.Avaliable,
                Img = item.Img
            };
            return View(itemVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditItemViewModel itemVM)
        {
            ViewBag.Categories = new SelectList(_categories.Category, "Id", "Name");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Ошибка изменения товара");
                return View("Edit", itemVM);
            }

            var item = await _itemsRepository.GetByIdAsyncNoTracking(id);

            if (item != null)
            {
                var itemUpd = new FoodItem
                {
                    Id = itemVM.Id,
                    Name = itemVM.Name,
                    Description = itemVM.Description,
                    Price = itemVM.Price,
                    Weight = itemVM.Weight,
                    Vendor = itemVM.Vendor,
                    Ingredients = itemVM.Ingredients,
                    CategoryId = itemVM.CategoryId,
                    Avaliable = itemVM.Avaliable,
                    Img = itemVM.Img
                };

                _itemsRepository.Update(itemUpd);

                return RedirectToAction("Index");
            }
            else return View(itemVM);
        }

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
        {
            ViewBag.Categories = new SelectList(_categories.Category, "Id", "Name");
            var foodItem = await _itemsRepository.GetByIdAsync(id);
            if (foodItem == null) return View("Error");
            return View(foodItem);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            ViewBag.Categories = new SelectList(_categories.Category, "Id", "Name");
            var foodItem = await _itemsRepository.GetByIdAsync(id);

            if (foodItem == null)
            {
                return View("Error");
            }

            _itemsRepository.Delete(foodItem);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddToFavourites(int id)
        {
            _favItems.Add(id);
			return RedirectToAction("Details", new { id });
		}

        [HttpPost]
        public async Task<IActionResult> RemoveFromFavourites(int id)
        {
            var favItem = await _favItems.GetItemById(id);
            _favItems.Delete(favItem);
            return RedirectToAction("Details", new { id }); 
		}
    }
}
