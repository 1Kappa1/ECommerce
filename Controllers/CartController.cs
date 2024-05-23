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
    public IActionResult Index()
    {
        var carrello = _context.Cart.ToList();
        return View(carrello);
    }
    
    // POST: Carrello/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
public async Task<IActionResult> AddMultiple(Dictionary<string, int> quantities)
{
    if (quantities == null || quantities.Count == 0)
    {
        return RedirectToAction("Index", "Fishing");
    }

    var carrelli = new List<Carrello>();
    foreach (var quantity in quantities)
    {
        var prodotto = _context.Oggetti.FirstOrDefault(p => p.Nome == quantity.Key);
        if (prodotto != null)
        {
            var carrello = new Carrello
            {
                NumeroDiOggetti = quantity.Value,
                Username_Utente = User.Identity!.Name!,
                Nome_Prodotto = prodotto.Nome
            };
            carrelli.Add(carrello);
        }
    }

    if (ModelState.IsValid)
    {
        foreach (var carrello in carrelli)
        {
            _context.Add(carrello);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    return RedirectToAction("Index", "Fishing");
}
}