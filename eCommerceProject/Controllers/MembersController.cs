using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.Controllers
{
    public class MembersController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }
    }

}
