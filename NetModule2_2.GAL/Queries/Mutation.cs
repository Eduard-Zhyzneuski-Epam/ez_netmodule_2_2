using NetModule2_2.BAL;
using NetModule2_2.GAL.Model;

namespace NetModule2_2.GAL.Queries
{
    public class Mutation
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;

        public Mutation(ICategoryService categoryService, IItemService itemService)
        {
            _categoryService = categoryService;
            _itemService = itemService;
        }

        public int AddCategory(NewCategory newCategory)
        {
            var categoryToAdd = Mapping.Map<GAL.Model.NewCategory, BAL.Category>(newCategory);
            return _categoryService.Add(categoryToAdd);
        }

        public int UpdateCategory(UpdatedCategory updatedCategory)
        {
            var categoryToUpdate = Mapping.Map<GAL.Model.UpdatedCategory, BAL.Category>(updatedCategory);
            _categoryService.Update(categoryToUpdate);
            return updatedCategory.Id;
        }

        public int DeleteCategory(int id) 
        {
            _categoryService.Delete(id);
            return id;
        }

        public int AddItem(NewItem newItem)
        {
            var itemToAdd = Mapping.Map<NewItem, BAL.Item>(newItem);
            return _itemService.Add(itemToAdd);
        }

        public int UpdateItem(UpdatedItem updatedItem)
        {
            var itemToUpdate = Mapping.Map<UpdatedItem, BAL.Item>(updatedItem);
            _itemService.Update(itemToUpdate);
            return itemToUpdate.Id;
        }

        public int DeleteItem(int id)
        {
            _itemService.Delete(id);
            return id;
        }
    }
}
