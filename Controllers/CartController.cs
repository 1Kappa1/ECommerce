using capanna.alessandro._5H.prenota.Models; using Microsoft.AspNetCore.Mvc; using System.Linq;

namespace capanna.alessandro._5H.prenota.Controllers { 
    public class CartController : Controller { 
        private readonly dataBase _context;

    public CartController(dataBase context) { _context = context; }

        public IActionResult Add(string? name) 
        { 
            return RedirectToAction("Index", "Fishing"); 
        } 
    } 
}