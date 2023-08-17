using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;

namespace ChefsAndDishes.Controllers;

public class DishController : Controller
{
    private readonly ILogger<DishController> _logger;
    // Add a private variable of type MyContext (or whatever you named your context file)
    private readonly MyContext _context;
    public DishController(ILogger<DishController> logger, MyContext context)
    {
        _logger = logger;
        // When our HomeController is instantiated, it will fill in _context with context
        // Remember that when context is initialized, it brings in everything we need from DbContext
        // which comes from Entity Framework Core
        _context = context;
    }



[HttpGet("")]
    public IActionResult Index()
    {
        List<Dish> AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
        return View("Index", AllDishes);
    }

    [HttpGet("/dishes")]
    public IActionResult DisplayNewDish()
    {
        return View("New"); //<--- HTML page to see our new, displayed dish
    }

    [HttpPost("dishes/new")] //<---- Create a new dish to input into db
    public IActionResult Create(Dish newDish)
    {
        if (ModelState.IsValid) //<--- validation 
        {
            _context.Add(newDish);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        else
        {
            // call the method to render the new page
            return DisplayNewDish();
        }
    }

    [HttpGet("Dishes/{id}")] //<--- Read added dishes from db
    public IActionResult Read(int id)
    {
        Dish? OneDish = _context.Dishes.FirstOrDefault(d => d.DishId == id);
        return View("Read", OneDish);
    }


    
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