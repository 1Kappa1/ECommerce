using System;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using capanna.alessandro._5H.prenota.Models;
using Microsoft.AspNetCore.DataProtection;
using capanna.alessandro._5H.prenota.ViewModels;
using Microsoft.AspNetCore.Identity;
using capanna.alessandro._5H.prenota.Controllers;


namespace capanna.alessandro._5H.prenota.Controllers
{
    public class FishingController : Controller
    {
        private readonly dataBase _context;

        public FishingController(dataBase context)
        {
            _context = context;
        }

        // GET: Fishing
        public async Task<IActionResult> Index()
        {
            return View(await _context.Oggetti.ToListAsync());
        }

        public async Task<IActionResult> Secret_Index()
        {
            return View(await _context.Oggetti.ToListAsync());
        }
        // GET: Fishing/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishing = await _context.Oggetti
                .FirstOrDefaultAsync(m => m.Nome == id);
            if (fishing == null)
            {
                return NotFound();
            }

            return View(fishing);
        }

        // GET: Fishing/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fishing/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [BindProperty]
        public IFormFile FileUpload { get; set; }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Descrizione,Prezzo,Img")] Fishing fishing)
        {
            if (ModelState.IsValid)
            {
                if (FileUpload == null || FileUpload.Length == 0)
                {
                    ModelState.AddModelError("FileUpload", "The file is required.");
                    return RedirectToAction("Secret_Index", "Fishing");
                }

                // Save the file to a specific folder
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string filePath = Path.Combine(folderPath, FileUpload.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(stream);
                }

                // Save the file name and extension to the database
                string fileName = FileUpload.FileName;
                string fileExtension = Path.GetExtension(FileUpload.FileName);

                fishing.Img = fileName;

                // Save to the database here
                _context.Add(fishing);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fishing);
        }

        // GET: Fishing/Edit/5
        // GET: Fishing/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishing = await _context.Oggetti.FindAsync(id);
            if (fishing == null)
            {
                return NotFound();
            }
            return View(fishing);
        }

        // POST: Fishing/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Nome,Descrizione,Prezzo,Img")] Fishing fishing)
        {
            try
            {
                Console.WriteLine($"FileUpload is null: {FileUpload == null}");
                Console.WriteLine($"FileUpload length: {FileUpload?.Length ?? 0}");
                // Check if FileUpload is not null before using it
                if (FileUpload != null && FileUpload.Length > 0)
                {
                    // Save the file to a specific folder
                    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    string filePath = Path.Combine(folderPath, FileUpload.FileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await FileUpload.CopyToAsync(stream);
                        }
                 
                    // Save the file name and extension to the database
                    string fileName = FileUpload.FileName;

                    fishing.Img = fileName;
                    Console.WriteLine(fishing.Img);
                }

                _context.Update(fishing);
                await _context.SaveChangesAsync();
                Console.WriteLine("Changes saved successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FishingExists(fishing.Nome))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Fishing/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishing = await _context.Oggetti
                .FirstOrDefaultAsync(m => m.Nome == id);
            if (fishing == null)
            {
                return NotFound();
            }

            return View(fishing);
        }

        // POST: Fishing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string? id)
        {
             var fishing = await _context.Oggetti
                .FirstOrDefaultAsync(m => m.Nome == id);
            if (fishing != null)
            {
                // Elimina il file associato all'oggetto
                if (System.IO.File.Exists(fishing.Img))
                {
                    System.IO.File.Delete(fishing.Img);
                }

                // Elimina l'oggetto dal database
                _context.Oggetti.Remove(fishing);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Secret_Index));
        }

        private bool FishingExists(string? id)
        {
            return _context.Oggetti.Any(e => e.Nome == id);
        }
    }
}
