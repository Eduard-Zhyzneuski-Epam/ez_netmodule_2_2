using NetModule2_2.DAL;
using NetModule2_2.EAL;
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
        private readonly IChangedItemEventPublisher changedItemEventPublisher;

        public ItemService(IItemRepository itemRepository, IChangedItemEventPublisher changedItemEventPublisher)
        {
            this.itemRepository = itemRepository;
            this.changedItemEventPublisher = changedItemEventPublisher;
        }

        public int Add(Item item)
        {
            ValidateItem(item);
            var dbItem = Mapping.Map<Item, DAL.Item>(item);
            return itemRepository.Add(dbItem);
        }

        public void Delete(int id)
        {
            itemRepository.Delete(id);
        }

        public Item Get(int id)
        {
            var dbItem = itemRepository.Get(id);
            if (dbItem is null)
                throw new ItemNotFoundException();
            return Mapping.Map<DAL.Item, Item>(dbItem);
        }

        public List<Item> List(int? categoryId, int pageSize, int pageNumber, out bool doWeHaveNextPage)
        {
            var dbItems = itemRepository.List(categoryId);
            doWeHaveNextPage = dbItems.Count < pageSize * pageNumber;
            return dbItems
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Select(dbItem => Mapping.Map<DAL.Item, Item>(dbItem)).ToList();
        }

        public void Update(Item item)
        {
            ValidateItem(item);
            if (itemRepository.Get(item.Id) is null)
                throw new ItemNotFoundException();

            var dbItem = Mapping.Map<Item, DAL.Item>(item);
            itemRepository.Update(dbItem);

            var changedItem = Mapping.Map<Item, EAL.ChangedItem>(item);
            changedItemEventPublisher.Publish(changedItem);
        }

        private void ValidateItem(Item item)
        {
            var errors = new List<string>();
            if (item.Name is null || item.Name == "")
                errors.Add("Item name is empty");
            if (item.Name.Length > 50)
                errors.Add("Item name is too long");
            if (item.CategoryId == 0)
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
