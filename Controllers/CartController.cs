using capanna.alessandro._5H.prenota.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
    
    [HttpPost]
public async Task<IActionResult> RemoveItem(string id)
{
    try
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("Id is required");
        }

        var fishing = await _context.Cart
            .FirstOrDefaultAsync(m => m.Nome_Prodotto == id);

        if (fishing == null)
        {
            return NotFound("Item not found");
        }

        _context.Cart.Remove(fishing);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    catch (Exception ex)
    {
        return StatusCode(500, "Error removing item: " + ex.Message);
    }
}

    // POST: Carrello/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMultiple(Dictionary<string, int> quantities)
    {
        if(User.Identity!.Name! != null)
        {
            if (quantities == null || quantities.Count == 0 )
            {
                return RedirectToAction("Index", "Fishing");
            }
            var carrelli = new List<Carrello>();
            foreach (var quantity in quantities)
            {
                var prodotto = _context.Oggetti.FirstOrDefault(p => p.Nome == quantity.Key);
                var carrello = _context.Cart.FirstOrDefault(c => c.Nome_Prodotto == prodotto.Nome && c.Username_Utente == User.Identity!.Name!);
                if (prodotto != null && quantity.Value > 0)
                {
                    if(!_context.Cart.Any(c => c.Nome_Prodotto == prodotto.Nome && c.Username_Utente == User.Identity!.Name!))
                    {
                        carrello = new Carrello
                        {
                            NumeroDiOggetti = quantity.Value,
                            Username_Utente = User.Identity!.Name!,
                            Nome_Prodotto = prodotto.Nome
                        };
                        carrelli.Add(carrello);
                    }
                    else
                    {
                        carrello.NumeroDiOggetti += quantity.Value;
                    }
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
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }


    public async Task<IActionResult> RemoveMultiple(Dictionary<string, int> quantities)
{
    if(User.Identity!.Name! != null)
    {
        if (quantities == null || quantities.Count == 0 )
        {
            return RedirectToAction("Index", "Fishing");
        }

        //Caricamento della lista carrelli
        var carrelli = new List<Carrello>();

        foreach (var quantity in quantities)
        {
            
            var prodotto = _context.Oggetti.FirstOrDefault(p => p.Nome == quantity.Key);
            var carrello = _context.Cart.FirstOrDefault(c => c.Nome_Prodotto == prodotto.Nome && c.Username_Utente == User.Identity!.Name!);
            if (prodotto != null)
            {
                Console.WriteLine(quantity);
                if(carrello != null)
                {
                    // Se il prodotto esiste nel carrello, modifica la quantità
                    carrello.NumeroDiOggetti = quantity.Value;
                }
            }
        }

        if (ModelState.IsValid)
        {
            foreach (var carrello in carrelli)
            {
                _context.Cart.Update(carrello);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        return RedirectToAction("Index", "Carrello");
    }
    else
    {
        return RedirectToAction("Login", "Account");
    }
}

}