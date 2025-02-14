﻿using eCommerceProject.Data;
using eCommerceProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceProject.Controllers
{
    public class CraftController : Controller
    {
        private readonly CraftContext _context;

        public CraftController (CraftContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Craft craft)
        {
            if (ModelState.IsValid)
            {
                // add to Db
                _context.Crafts.Add(craft); // Prepares Insert
                _context.SaveChanges(); // Execute pending Insert

                // Show success message on page
                // This creates a message object to be able to be used in the view page
                ViewData["Message"] = $"{craft.Title} was added successfully";
                return View();
            }
            return View(craft);
        }

    }
}
