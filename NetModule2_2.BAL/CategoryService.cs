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

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void Add(Category category)
        {
            ValidateCategory(category);
            var dbCategory = new DAL.Category
            {
                Name = category.Name,
                Image = category.Image,
                ParentCategoryName = category.ParentCategory?.Name
            };
            categoryRepository.Add(dbCategory);
        }

        public void Delete(string name)
        {
            categoryRepository.Delete(name);
        }

        public Category Get(string name)
        {
            var dbCategory = categoryRepository.Get(name);
            if (dbCategory is null)
                throw new CategoryNotFoundException();
            var leaf = new Category { Name = dbCategory.Name, Image = dbCategory.Image };
            var currentCategory = leaf;
            while (dbCategory.ParentCategoryName != null)
            {
                dbCategory = categoryRepository.Get(dbCategory.ParentCategoryName);
                var newRootCategory = new Category { Name = dbCategory.Name, Image = dbCategory.Image };
                currentCategory.ParentCategory = newRootCategory;
                currentCategory = newRootCategory;
            }
            return leaf;
        }

        public List<Category> List()
        {
            var dbCategories = categoryRepository.List();
            return dbCategories.Select(dbCategory =>
            {
                var leaf = new Category { Name = dbCategory.Name, Image = dbCategory.Image };
                var currentCategory = leaf;
                while (dbCategory.ParentCategoryName != null)
                {
                    dbCategory = dbCategories.First(c => c.Name == dbCategory.ParentCategoryName);
                    var newRootCategory = new Category { Name = dbCategory.Name, Image = dbCategory.Image };
                    currentCategory.ParentCategory = newRootCategory;
                    currentCategory = newRootCategory;
                }
                return leaf;
            }).ToList();
        }

        public void Update(Category category)
        {
            ValidateCategory(category);
            if (categoryRepository.Get(category.Name) is null)
                throw new CategoryNotFoundException();
            var dbCategory = new DAL.Category
            {
                Name = category.Name,
                Image = category.Image,
                ParentCategoryName = category.ParentCategory?.Name
            };
            categoryRepository.Update(dbCategory);
        }

        private void ValidateCategory(Category category)
        {
            var errors = new List<string>();
            if (category.Name is null || category.Name == "")
                errors.Add("Empty category name");
            if (category.Name.Length > 50)
                errors.Add("Category name too long");
            if (errors.Any())
                throw new InvalidCategoryException(string.Join("; ", errors));
        }
    }
}
