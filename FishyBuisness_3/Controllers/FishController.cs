using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FishyBuisness_3.Data;
using FishyBuisness_3.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc.Localization;




namespace FishyBuisness_3.Controllers
{
    public class FishController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHtmlLocalizer<HomeController> _localizer;

        public FishController(ApplicationDbContext context, IHtmlLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        // GET: Fish
        public async Task<IActionResult> Index(string Sorting_Order,string searchString)
        {
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "FishNameDesc" : "";
            ViewBag.SortingPrice = Sorting_Order == "PriceAsc" ? "PriceDsc" : "PriceAsc";
            var fish = from f in _context.Fish.Include(f => f.Environment)
                       select f;
            switch (Sorting_Order)
            {
                case "FishNameDesc":
                    fish = fish.OrderByDescending(f => f.FishName);
                    break;
                case "PriceDsc":
                    fish = fish.OrderByDescending(f =>f.Price);
                    break;
                case "PriceAsc":
                    fish = fish.OrderBy(f => f.Price);
                    break;
                default:
                    fish = fish.OrderBy(f => f.FishName);
                    break;
                    
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                fish = fish.Where(s => s.FishName.Contains(searchString));
            }
            ViewData["FishName"] = _localizer["FishName"];
            return View(await fish.ToListAsync());
        }

        // GET: Fish/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fish = await _context.Fish
                .Include(f => f.Environment)
                .FirstOrDefaultAsync(m => m.FishId == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // GET: Fish/Create
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FishId,FishName,FishDescription,Spieces,Habitat,EnvironmentId,Lenght,Weight,Price")] Fish fish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fish);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", fish.EnvironmentId);
            return View(fish);
        }

        // GET: Fish/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fish = await _context.Fish.FindAsync(id);
            if (fish == null)
            {
                return NotFound();
            }
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", fish.EnvironmentId);
            return View(fish);
        }

        // POST: Fish/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FishId,FishName,FishDescription,Spieces,Habitat,EnvironmentId,Lenght,Weight,Price")] Fish fish)
        {
            if (id != fish.FishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishExists(fish.FishId))
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
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", fish.EnvironmentId);
            return View(fish);
        }

        // GET: Fish/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fish = await _context.Fish
                .Include(f => f.Environment)
                .FirstOrDefaultAsync(m => m.FishId == id);
            if (fish == null)
            {
                return NotFound();
            }

            return View(fish);
        }

        // POST: Fish/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Fish = await _context.Fish.FindAsync(id);

            if (Fish == null)
            {
                return NotFound();
            }

            // Sprawdzenie czy klucz jest używany w innych miejscach to o co nam chodzi
            var isUsed = _context.Stocks.Any(s => s.FishId == id);
            if (isUsed)
            {
                string errorMessage = "You cannot delete it because this fishtank is used in other tabs.";
                ModelState.AddModelError("ErrorMessage", errorMessage);
                return View("Delete", Fish);
                // Przekierowanie do widoku "Delete" z danymi environment, aby można było wyświetlić komunikat
            }


            // Normalna ścieżka, jeśli nie ma błędów
            _context.Fish.Remove(Fish);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); ;
        }

        private bool FishExists(int id)
        {
            return _context.Fish.Any(e => e.FishId == id);
        }
    }
}
