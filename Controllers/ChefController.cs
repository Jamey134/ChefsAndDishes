using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefsAndDishes.Models;
using Microsoft.AspNetCore.Identity;
//Added for session check
using Microsoft.AspNetCore.Mvc.Filters;


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

        return View();

    }

    // [HttpGet("Dashboard")]
    // public IActionResult Dashboard()
    // {
    //     return View("Dashboard");

    // }



    }



