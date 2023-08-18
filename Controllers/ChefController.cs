using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.AspNetCore.Identity;
//Added for session check
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers;


public class ChefController : Controller
{
    private readonly ILogger<ChefController> _logger;

    private readonly MyContext db;

    public ChefController(ILogger<ChefController> logger, MyContext context)
    {
        _logger = logger;
        db = context;
    }

    [HttpGet("")]
    public IActionResult Index()
    {
        List<Chef> allChefs = db.Chefs.Include(ad => ad.AllDishes).ToList(); //<---- Created this to render all Chefs that include their dishes
        Console.WriteLine("---------------------->" + allChefs);
        return View("Index", allChefs);
    }

    [HttpGet("chefs/new")] //<--- To render AddChef.cshtml page
    public IActionResult AddChef()
    {
        return View("AddChef");
    }

    [HttpPost("chefs/create")] // <--- Method to add a chef to our db (CREATE)
    public IActionResult CreateChef(Chef newChef)
    {
        if (ModelState.IsValid)
        {
            db.Add(newChef); // Adds Chef's info into DB
            db.SaveChanges(); // Saves Chef's info in DB
            return RedirectToAction("Index"); // Displays info onto Chef's Index page
        }
        else
        {

            return View("AddChef"); // Returns user to add chef page due to invalid validations
        }
    }

}



