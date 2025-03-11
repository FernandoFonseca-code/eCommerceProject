using eCommerceProject.Data;
using eCommerceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.Controllers;

public class CartController : Controller
{
    private readonly CraftContext _context;
    private const string Cart = "ShoppingCart";

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

        CraftCartViewModel craftCart = new()
        {
            CraftId = craftToCart.CraftId,
            Title = craftToCart.Title,
            Price = (double)craftToCart.Price
        };
        // Creates a list of all the crafts in the cart
        List<CraftCartViewModel> CartCrafts = GetExistingCartData();
        CartCrafts.Add(craftCart);
        CreateCartCookie(CartCrafts);

        TempData["Message"] = $"{craftToCart.Title} was added to your cart";
        return RedirectToAction("Index", "Craft");
    }
    /// <summary>
    /// Creates cookie with list of crafts from the cart
    /// </summary>
    /// <param name="CartCrafts"></param>
    private void CreateCartCookie(List<CraftCartViewModel> CartCrafts)
    {
        // Creates a cookie with the list of crafts in the cart
        String cookieData = System.Text.Json.JsonSerializer.Serialize(CartCrafts);
        /// Adds the cookie to the response
        HttpContext.Response.Cookies.Append(Cart, cookieData, new CookieOptions()
        {
            Expires = DateTime.Now.AddDays(1)
        });
    }

    /// <summary>
    /// Return the current list of crafts in the user's shopping cart
    /// cookie. If there is no cookie, an empty list will be returned 
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private List<CraftCartViewModel> GetExistingCartData()
    {
        string? cookie = HttpContext.Request.Cookies[Cart];
        if (string.IsNullOrEmpty(cookie))
        {
            return new List<CraftCartViewModel>();
        }
        else
        {
            return System.Text.Json.JsonSerializer.Deserialize<List<CraftCartViewModel>>(cookie);
        }
    }

    public IActionResult Summary()
    {
        List<CraftCartViewModel> CartCrafts = GetExistingCartData();
        return View(CartCrafts);
    }

    public IActionResult Remove(int id)
    {
        List<CraftCartViewModel> CartCrafts = GetExistingCartData();

        CraftCartViewModel? targetCraft = CartCrafts.FirstOrDefault(c => c.CraftId == id);

        CartCrafts.Remove(targetCraft);

        CreateCartCookie(CartCrafts);

        return RedirectToAction(nameof(Summary));
    }
}
