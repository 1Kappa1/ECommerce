using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using capanna.alessandro._5H.prenota.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Mail;
using System.Net;


namespace capanna.alessandro._5H.prenota.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

