using Microsoft.AspNetCore.Mvc;
using NetModule2_2.BAL;
using NetModule2_2.DAL;
using NetModule2_2.NAL.Models;

namespace NetModule2_2.NAL.Controllers
{
    [ApiController]
    public class ItemController : Controller
    {
        private const int pageSize = 20;
        private readonly IItemService itemService;

        public ItemController(IItemService itemService)
        {
            this.itemService = itemService;
        }

        [HttpGet("/items/category/{categoryId}/pages/{pageNumber}")]
        [BuyerAccess]
        public IActionResult ListPagedByCategory([FromRoute] int categoryId, [FromRoute] int pageNumber)
        {
            return List(categoryId, pageNumber);
        }

        [HttpGet("/items/pages/{pageNumber}")]
        [BuyerAccess]
        public IActionResult ListPaged([FromRoute] int pageNumber)
        {
            return List(null, pageNumber);
        }

        private IActionResult List(int? categoryId, int pageNumber)
        {
            var rawItems = itemService.List(categoryId, pageSize, pageNumber, out var doWeHaveNextPage);
            var items = rawItems.Select(item => Mapping.Map<BAL.Item, NAL.Models.Item>(item));
            var itemsList = new ItemsList
            {
                Items = items.ToList(),
                NextPageLink = doWeHaveNextPage ? ResourceNavigator.ItemsLink(categoryId, pageNumber + 1) : null,
                PreviousPageLink = pageNumber > 1 ? ResourceNavigator.ItemsLink(categoryId, pageNumber - 1) : null
            };
            return Ok(itemsList);
        }

        [HttpGet("/item/{id}")]
        [BuyerAccess]
        public IActionResult Get([FromRoute] int id)
        {
            var rawItem = itemService.Get(id);
            var item = Mapping.Map<BAL.Item, NAL.Models.Item>(rawItem);
            return Ok(item);
        }

        [HttpPost("/item")]
        [ManagerAccess]
        public IActionResult Create([FromBody] NewItem item)
        {
            var rawItem = Mapping.Map<NewItem, BAL.Item>(item);
            var newItemId = itemService.Add(rawItem);
            var newItemLink = ResourceNavigator.ItemLink(newItemId);
            return new CreatedAtActionResult(null, null, null, newItemLink);
        }

        [HttpPut("/item/{id}")]
        [ManagerAccess]
        public IActionResult Update([FromBody] UpdatedItem item, [FromRoute] int id)
        {
            var rawItem = Mapping.Map<UpdatedItem, BAL.Item>(item);
            rawItem.Id = id;
            itemService.Update(rawItem);
            return NoContent();
        }

        [HttpDelete("/item/{id}")]
        [ManagerAccess]
        public IActionResult Delete([FromRoute] int id)
        {
            itemService.Delete(id);
            return NoContent();
        }
    }
}
