using NetModule2_2.BAL;

namespace NetModule2_2.GAL.Queries
{
    public class Query
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;

        public Query(ICategoryService categoryService, IItemService itemService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
        }

        public List<GAL.Model.Category> GetCategories()
        {
            var rawList = _categoryService.List();
            return rawList.Select(rawCategory => Mapping.Map<BAL.Category, GAL.Model.Category>(rawCategory)).ToList();
        }
        
        public GAL.Model.Category GetCategory(int id)
        {
            var rawCategory = _categoryService.Get(id);
            return Mapping.Map<BAL.Category, GAL.Model.Category>(rawCategory);
        }

        public List<GAL.Model.Item> GetItems(int? categoryId, int pageNumber, int pageSize)
        {
            var rawList = _itemService.List(categoryId, pageSize, pageNumber, out _);
            var rawCategories = _categoryService.List();
            var categories = rawCategories.Select(rawCategory => Mapping.Map<BAL.Category, GAL.Model.Category>(rawCategory)).ToList();
            return rawList
                .Select(rawItem => Mapping.Map<BAL.Item, GAL.Model.Item>(rawItem))
                .Select(item =>
                {
                    var categoryId = rawList.First(i => i.Id == item.Id).CategoryId;
                    var category = categories.First(c => c.Id == categoryId);
                    item.Category = category;
                    return item;
                })
                .ToList();
        }

        public GAL.Model.Item GetItem(int id)
        {
            var rawItem = _itemService.Get(id);
            var category = GetCategory(rawItem.CategoryId);
            var item = Mapping.Map<BAL.Item, GAL.Model.Item>(rawItem);
            item.Category = category;
            return item;
        }
    }
}
