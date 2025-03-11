using eCommerceProject.Data;
using eCommerceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.Controllers
{
    public class CartController : Controller
    {
        private readonly CraftContext _context;

        public CartController(CraftContext context )
        {
            _context = context;
        }

        public IActionResult Add(int id)
        {
            Craft? craftToCart = _context.Crafts.Where(c => c.CraftId == id).SingleOrDefault();

            if (craftToCart == null)
            {

                TempData["Message"] = "Apologies, that craft is no longer exists";
                return RedirectToAction("Index", "Craft");
            }
            
            TempData["Message"] = $"{craftToCart.Title} was added to your cart";
            return RedirectToAction("Index", "Craft");
        }
    }
}
