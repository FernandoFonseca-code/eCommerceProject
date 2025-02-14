using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.Controllers
{
    public class CraftController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}
