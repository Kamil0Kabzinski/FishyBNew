using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FishyBuisness_3.Data;
using FishyBuisness_3.Models;
using Environment = FishyBuisness_3.Models.Environment;


namespace FishyBuisness_3.Controllers
{
    public class EnvironmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnvironmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Environments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Environments.ToListAsync());
        }

        // GET: Environments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environment = await _context.Environments
                .FirstOrDefaultAsync(m => m.EnvironmentId == id);
            if (environment == null)
            {
                return NotFound();
            }

            return View(environment);
        }

        // GET: Environments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Environments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EnvironmentId,Name,Description")] Environment environment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(environment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(environment);
        }

        // GET: Environments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environment = await _context.Environments.FindAsync(id);
            if (environment == null)
            {
                return NotFound();
            }
            return View(environment);
        }

        // POST: Environments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EnvironmentId,Name,Description")] Models.Environment environment)
        {
            if (id != environment.EnvironmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(environment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnvironmentExists(environment.EnvironmentId))
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
            return View(environment);
        }

        // GET: Environments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var environment = await _context.Environments
                .FirstOrDefaultAsync(m => m.EnvironmentId == id);
            if (environment == null)
            {
                return NotFound();
            }

            return View(environment);
        }

        // POST: Environments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var environment = await _context.Environments.FindAsync(id);

            if (environment == null)
            {
                return NotFound();
            }

            // Sprawdzenie czy klucz jest używany w innych miejscach to o co nam chodzi
            var isUsed = _context.FishTanks.Any(ft => ft.EnvironmentId == id) ||
                         _context.Fish.Any(f => f.EnvironmentId == id) ||
                         _context.Stocks.Any(s => s.EnvironmentId == id);
            if (isUsed)
            {
                string errorMessage = "You cannot delete it because this environment is used in other tabs.";
                ModelState.AddModelError("ErrorMessage", errorMessage);
                return View("Delete", environment); 
                // Przekierowanie do widoku "Delete" z danymi environment, aby można było wyświetlić komunikat
            }


            // Normalna ścieżka, jeśli nie ma błędów
            _context.Environments.Remove(environment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool EnvironmentExists(int id)
        {
            return _context.Environments.Any(e => e.EnvironmentId == id);
        }


    }
}
