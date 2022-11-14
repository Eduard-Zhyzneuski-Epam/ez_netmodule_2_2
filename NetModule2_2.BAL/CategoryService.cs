using NetModule2_2.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetModule2_2.BAL
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IItemRepository itemRepository;

        public CategoryService(ICategoryRepository categoryRepository, IItemRepository itemRepository)
        {
            this.categoryRepository = categoryRepository;
            this.itemRepository = itemRepository;
        }

        public int Add(Category category)
        {
            ValidateCategory(category);
            var dbCategory = Mapping.Map<Category, DAL.Category>(category);
            return categoryRepository.Add(dbCategory);
        }

        public void Delete(int id)
        {
            itemRepository.DeleteCategory(id);
            categoryRepository.Delete(id);
        }

        public Category Get(int id)
        {
            var dbCategory = categoryRepository.Get(id);
            if (dbCategory is null)
                throw new CategoryNotFoundException();
            return Mapping.Map<DAL.Category, Category>(dbCategory);
        }

        public List<Category> List()
        {
            var dbCategories = categoryRepository.List();
            return dbCategories.Select(dbCategory => Mapping.Map<DAL.Category, Category>(dbCategory)).ToList();
        }

        public void Update(Category category)
        {
            ValidateCategory(category);
            if (categoryRepository.Get(category.Id) is null)
                throw new CategoryNotFoundException();
            var dbCategory = Mapping.Map<Category, DAL.Category>(category);
            categoryRepository.Update(dbCategory);
        }

        private void ValidateCategory(Category category)
        {
            var errors = new List<string>();
            if (category.Name is null || category.Name == "")
                errors.Add("Empty category name");
            if (category.Name.Length > 50)
                errors.Add("Category name too long");
            if (category.ParentCategoryId is not null && categoryRepository.Get(category.ParentCategoryId.Value) is null)
                errors.Add("Parent category does not exist");
            if (errors.Any())
                throw new InvalidCategoryException(string.Join("; ", errors));
        }
    }
}
