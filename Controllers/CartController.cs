using capanna.alessandro._5H.prenota.Models;
using Microsoft.AspNetCore.Mvc;

public class CarrelloController : Controller
{
    private readonly dataBase _context;

    public CarrelloController(dataBase context)
    {
        _context = context;
    }

    // GET: Carrello
    public async Task<IActionResult> Index()
    {
        var carrello = _context.Cart.ToList();
        return View(carrello);
    }
    
    // POST: Carrello/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(List<Carrello> carrelli)
    {
        if (ModelState.IsValid)
        {
            foreach (var carrello in carrelli)
            {
                carrello.Username_Utente = User.Identity!.Name!;
                _context.Add(carrello);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return RedirectToAction("Index","Fishing");
    }
}