using eCommerceProject.Data;
using eCommerceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.Controllers
{
    public class MembersController : Controller
    {
        private readonly CraftContext _context;

        public MembersController(CraftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            if (ModelState.IsValid)
            {
                /// Map RegisterViewModel data to Member object
                Member member = new()
                {
                    Email = regModel.Email,
                    Password = regModel.Password
                };

                _context.Members.Add(member);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Home");
            }

            return View(regModel);
        }
    }

}
