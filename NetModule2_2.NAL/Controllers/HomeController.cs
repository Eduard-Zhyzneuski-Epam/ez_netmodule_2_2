using Microsoft.AspNetCore.Mvc;

namespace NetModule2_2.NAL.Controllers
{
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            var catalog = new
            {
                CategoriesLink = ResourceNavigator.CategoriesLink(),
                ItemsLink = ResourceNavigator.ItemsLink(null, 1)
            };
            return Ok(catalog);
        }
    }
}
