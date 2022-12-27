using Microsoft.AspNetCore.Mvc;
using NetModule2_2.BAL;
using NetModule2_2.NAL.Models;

namespace NetModule2_2.NAL.Controllers
{
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService) 
        {
            this.categoryService = categoryService;
        }

        [HttpGet("category/{id}")]
        [BuyerAccess]
        public IActionResult Get([FromRoute] int id) 
        { 
            try
            {
                var rawCategory = categoryService.Get(id);
                var category = Mapping.Map<BAL.Category, NAL.Models.Category>(rawCategory);
                return Ok(category);
            }
            catch (CategoryNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("categories")]
        [BuyerAccess]
        public IActionResult List()
        {
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type}:{claim.Value}");
            }
            var rawCategories = categoryService.List();
            var categories = rawCategories.Select(c => Mapping.Map<BAL.Category, NAL.Models.Category>(c));
            return Ok(categories);
        }

        [HttpPost("category")]
        [ManagerAccess]
        public IActionResult Create([FromBody] NewCategory category)
        {
            var rawCategory = Mapping.Map<NewCategory, BAL.Category>(category);
            var newCategoryId = categoryService.Add(rawCategory);
            var newResourceLink = ResourceNavigator.CategoryLink(newCategoryId);
            return new CreatedAtActionResult(null, null, null, newResourceLink);
        }

        [HttpPut("category")]
        [ManagerAccess]
        public IActionResult Update([FromBody] UpdatedCategory category) 
        { 
            var rawCategory = Mapping.Map<UpdatedCategory, BAL.Category>(category);
            categoryService.Update(rawCategory);
            return NoContent();
        }

        [HttpDelete("category/{id}")]
        [ManagerAccess]
        public IActionResult Delete([FromRoute] int id)
        {
            categoryService.Delete(id);
            return NoContent();
        }
    }
}