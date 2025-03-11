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
            // Map RegisterViewModel data to Member object
            Member newMember = new()
            {
                Email = regModel.Email,
                Password = regModel.Password
            };

            _context.Members.Add(newMember);
            await _context.SaveChangesAsync();
            LogUserIn(newMember.Email);

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
            // Check if the newMember exists in the database
            // LINQ query to check if the newMember exists in the database
            Member? member = await _context.Members
                .Where(m => m.Email == loginModel.Email && m.Password == loginModel.Password)
                .FirstOrDefaultAsync();

            // Traditional SQL query to check if the newMember exists in the database
            // Member? m = await (from newMember in _context.Members
            //                   where newMember.Email == loginModel.Email &&
            //                          newMember.Password == loginModel.Password
            //                   select newMember).SingleOrDefaultAsync();

            // If the newMember exists, redirect to the home page
            if (member != null)
            {
                LogUserIn(loginModel.Email);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
            }
        }
        return View(loginModel);
    }
    /// <summary>
    /// This method logs the user in by setting the email value in the session object
    /// </summary>
    /// <param name="email"></param>
    private void LogUserIn(string email)
    {
        HttpContext.Session.SetString("Email", email);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Remove("Email");
        return RedirectToAction("Index", "Home");
    }
}
