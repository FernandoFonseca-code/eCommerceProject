using eCommerceProject.Data;
using eCommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.Controllers;

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

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginModel)
    {
        if (ModelState.IsValid)
        {
            /// Check if the member exists in the database
            /// LINQ query to check if the member exists in the database
            Member? member = await _context.Members
                .Where(m => m.Email == loginModel.Email && m.Password == loginModel.Password)
                .FirstOrDefaultAsync();

            /// Traditional SQL query to check if the member exists in the database
            /// Member? m = await (from member in _context.Members
            ///                   where member.Email == loginModel.Email &&
            ///                          member.Password == loginModel.Password
            ///                   select member).SingleOrDefaultAsync();

            /// If the member exists, redirect to the home page
            if (member != null)
            {
                HttpContext.Session.SetString("Email", loginModel.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
            }
        }
        return View(loginModel);
    }
}
