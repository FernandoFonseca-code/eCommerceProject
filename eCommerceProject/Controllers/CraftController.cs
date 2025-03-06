using eCommerceProject.Data;
using eCommerceProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceProject.Controllers;

public class CraftController : Controller
{
    private readonly CraftContext _context;

    public CraftController(CraftContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Shows all crafts using asyncronous code
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        // Get all crafts from Db
        List<Craft> crafts = await _context.Crafts.ToListAsync();
        // Show them on page
        //return View(_context.Crafts.ToList());
        return View(crafts);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    /// <summary>
    /// For async code information see:
    /// https://learn.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro?view=aspnetcore-9.0#asynchronous-code
    public async Task<IActionResult> Create(Craft craft)
    {
        if (ModelState.IsValid)
        {
            // add to Db
            _context.Crafts.Add(craft);         // Prepares Insert
            await _context.SaveChangesAsync(); // Execute pending Insert

            // Show success message on page
            // This creates a message object to be able to be used in the view page
            ViewData["Message"] = $"{craft.Title} was added successfully";
            return View();
        }
        return View(craft);
    }

    [HttpGet]
    /// <summary>
    /// This is pulling the id from the URL that the user clicked on and displaying
    /// the edit page with the craftModel that was selected
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Edit(int id)
    {
        Craft? crafToEdit = await _context.Crafts.FindAsync(id);
        if (crafToEdit == null)
        {
            /// NotFound is a built in method that returns a 404 error
            return NotFound();
        }
        return View(crafToEdit);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Craft craftModel)
    {
        if (ModelState.IsValid)
        {
            _context.Crafts.Update(craftModel);
            await _context.SaveChangesAsync();
            TempData["Message"] = $"{craftModel.Title} was updated successfully";
            return RedirectToAction("Index");
        }
        return View(craftModel);
    }
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        Craft? craftToDelete = await _context.Crafts.FindAsync(id);
        
        if (craftToDelete == null)
        {
            return NotFound();
        }

        return View(craftToDelete);
    }
    /// <summary>
    /// This is the post method for deleting a craftDetails. It is called when the user clicks the delete button.
    /// It has an Action name of Delete to differentiate it from the get method because in C# you can't 
    /// have two methods with the same name and parameter.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        Craft craftToDelete = await _context.Crafts.FindAsync(id);
        
        if (craftToDelete != null)
        {
            _context.Crafts.Remove(craftToDelete);
            await _context.SaveChangesAsync();
            TempData["Message"] = $"{craftToDelete.Title} was deleted successfully";
            return RedirectToAction("Index");
        }
        
        TempData["ErrorMessage"] = "This craftDetails was already deleted";
        return RedirectToAction("Index");
    }
    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        Craft? craftDetails = await _context.Crafts.FindAsync(id);
        if (craftDetails == null)
        {
            return NotFound();
        }
        return View(craftDetails);
    }
}
