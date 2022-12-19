using Microsoft.EntityFrameworkCore;
using ZapProject.Data.Interfaces;
using ZapProject.Models;

namespace ZapProject.Data.Repository
{
	public class HomeRepository : IHomeService
	{
		private readonly ApplicationDbContext _context;

		public HomeRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		
		public async Task<List<FoodItem>> GetPopularItems()
		{
			var itemsAmountBuff = await _context.OrderItems.Select(i => new KeyValuePair<int, int>(i.ItemId, i.Amount)).ToListAsync();
			//List<int> orderItems = new List<int>();
			List<FoodItem> items = new List<FoodItem>();

			if (itemsAmountBuff.Count != 0)
			{
				for (int i = 0; i < itemsAmountBuff.Count; i++)
				{
					for (int j = i + 1; j < itemsAmountBuff.Count; j++)
					{
						if (itemsAmountBuff[j].Key == itemsAmountBuff[i].Key)
						{
							itemsAmountBuff[i] = new KeyValuePair<int, int>(itemsAmountBuff[i].Key, itemsAmountBuff[i].Value + itemsAmountBuff[j].Value);
							itemsAmountBuff.Remove(itemsAmountBuff[j]);
							j--;
						}
					}
				}

				// SOLUTION 1

				//Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
				//for (int i = 0; i < itemsAmountBuff.Count; i++)
				//{
				//	if (keyValuePairs.ContainsKey(itemsAmountBuff[i].Key))
				//	{
				//		keyValuePairs[itemsAmountBuff[i]] += itemsAmountBuff[i].Value;
				//	}
				//	else
				//	{
				//		keyValuePairs.Add(itemsAmountBuff[i].Key, itemsAmountBuff[i].Value);
				//	}
				//}

				// SOLUTION 2

				//for (int i = 0; i < orderItems.Count; i++)
				//{
				//	int itemCount = 0;
				//	for (int j = 0; j < orderItems.Count; j++)
				//	{
				//		if (orderItems[j] == orderItems[i])
				//		{
				//			itemCount++;
				//		}
				//	}
				//	if (keyValuePairs.ContainsKey(orderItems[i]))
				//	{
				//		keyValuePairs[orderItems[i]] += itemCount;
				//	}
				//	else
				//	{
				//		keyValuePairs.Add(orderItems[i], itemCount);
				//	}
				//}
				//
				//var itemsId = from kvp in itemsAmountBuff
				//			 orderby kvp.Value descending
				//			 select kvp.Key;

				itemsAmountBuff = itemsAmountBuff.OrderByDescending(kvp => kvp.Value).ToList();

				foreach (var kvp in itemsAmountBuff)
				{
					items.Add(_context.FoodItems.FirstOrDefault(i => i.Id == kvp.Key));
				}
			}
			return items;
		}
	}
}
