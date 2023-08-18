using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private readonly MyContext db;
    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        db = context;
    }


    [HttpGet("/dishes")]
    public IActionResult DisplayDishes()
    {
        List<Dish> AllDishes = db.Dishes.Include(c => c.Creator).OrderByDescending(d => d.CreatedAt).ToList();
        return View("Index", AllDishes); //<--- HTML page to see our new, displayed dish
    }

    [HttpPost("dishes/new")] //<---- Create a new dish to input into db
    public IActionResult Create(Dish newDish)
    {
        if (ModelState.IsValid) //<--- validation 
        {
            db.Add(newDish);
            db.SaveChanges();
            return RedirectToAction("DisplayDishes");
        }
        else
        {
            List<Chef> AllChefs = db.Chefs.ToList();
            ViewBag.AllChefs = AllChefs;
            // call the method to render the new page
            return View("AddDish");
        }
    }

    [HttpGet("dishes/create")]
    public IActionResult AddDish()
    {
        List<Chef> AllChefs = db.Chefs.ToList();
        ViewBag.AllChefs = AllChefs;
        return View();
    }

    // [HttpGet("Dishes/{id}")] //<--- Read added dishes from db
    // public IActionResult Read(int id)
    // {
    //     Dish? OneDish = db.Dishes.FirstOrDefault(d => d.DishId == id);
    //     return View("Read", OneDish);
    // }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}