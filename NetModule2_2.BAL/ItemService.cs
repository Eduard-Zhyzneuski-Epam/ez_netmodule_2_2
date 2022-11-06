using NetModule2_2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.BAL
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository itemRepository;
        private readonly ICategoryService categoryService;

        public ItemService(IItemRepository itemRepository, ICategoryService categoryService)
        {
            this.itemRepository = itemRepository;
            this.categoryService = categoryService;
        }

        public void Add(Item item)
        {
            ValidateItem(item);
            var dbItem = new DAL.Item
            {
                Name = item.Name,
                CategoryName = item.Category.Name,
                Amount = item.Amount,
                Description = item.Description,
                Image = item.Image,
                Price = (double)item.Price
            };
            itemRepository.Add(dbItem);
        }

        public void Delete(string name)
        {
            itemRepository.Delete(name);
        }

        public Item Get(string name)
        {
            var dbItem = itemRepository.Get(name);
            if (dbItem is null)
                throw new ItemNotFoundException();
            var category = categoryService.Get(dbItem.CategoryName);
            return new Item
            {
                Name = dbItem.Name,
                Amount = dbItem.Amount,
                Description = dbItem.Description,
                Category = category,
                Image = dbItem.Image,
                Price = (decimal)dbItem.Price
            };
        }

        public List<Item> List()
        {
            var dbItems = itemRepository.List();
            var categories = categoryService.List();
            return dbItems.Select(i => new Item
            {
                Name = i.Name,
                Amount = i.Amount,
                Category = categories.First(c => c.Name == i.CategoryName),
                Description = i.Description,
                Price = (decimal)i.Price,
                Image = i.Image
            }).ToList();
        }

        public void Update(Item item)
        {
            ValidateItem(item);
            if (itemRepository.Get(item.Name) is null)
                throw new ItemNotFoundException();
            var dbItem = new DAL.Item
            {
                Name = item.Name,
                CategoryName = item.Category.Name,
                Amount = item.Amount,
                Description = item.Description,
                Image = item.Image,
                Price = (double)item.Price
            };
            itemRepository.Add(dbItem);
        }

        private void ValidateItem(Item item)
        {
            var errors = new List<string>();
            if (item.Name is null || item.Name == "")
                errors.Add("Item name is empty");
            if (item.Name.Length > 50)
                errors.Add("Item name is too long");
            if (item.Category is null || item.Category.Name is null || item.Category.Name == "")
                errors.Add("Category required");
            if (item.Price < 0)
                errors.Add("Price should be non-negative");
            if (item.Amount <= 0)
                errors.Add("Amount should be positive");
            if (errors.Any())
                throw new InvalidItemException(string.Join("; ", errors));
        }
    }
}
