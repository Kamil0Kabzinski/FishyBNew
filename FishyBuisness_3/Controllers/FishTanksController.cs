using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FishyBuisness_3.Data;
using FishyBuisness_3.Models;

namespace FishyBuisness_3.Controllers
{
    public class FishTanksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FishTanksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FishTanks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.FishTanks.Include(f => f.Environment);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: FishTanks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishTank = await _context.FishTanks
                .Include(f => f.Environment)
                .FirstOrDefaultAsync(m => m.FishTankId == id);
            if (fishTank == null)
            {
                return NotFound();
            }

            return View(fishTank);
        }

        // GET: FishTanks/Create
        public IActionResult Create()
        {
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name");
            return View();
        }

        // POST: FishTanks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FishTankId,Name,Descirption,Capacity,EnvironmentId")] FishTank fishTank)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fishTank);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", fishTank.EnvironmentId);
            return View(fishTank);
        }

        // GET: FishTanks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishTank = await _context.FishTanks.FindAsync(id);
            if (fishTank == null)
            {
                return NotFound();
            }
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", fishTank.EnvironmentId);
            return View(fishTank);
        }

        // POST: FishTanks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FishTankId,Name,Descirption,Capacity,EnvironmentId")] FishTank fishTank)
        {
            if (id != fishTank.FishTankId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fishTank);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FishTankExists(fishTank.FishTankId))
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
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", fishTank.EnvironmentId);
            return View(fishTank);
        }

        // GET: FishTanks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fishTank = await _context.FishTanks
                .Include(f => f.Environment)
                .FirstOrDefaultAsync(m => m.FishTankId == id);
            if (fishTank == null)
            {
                return NotFound();
            }

            return View(fishTank);
        }

        // POST: FishTanks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var FishTanks = await _context.FishTanks.FindAsync(id);

            if (FishTanks == null)
            {
                return NotFound();
            }

            // Sprawdzenie czy klucz jest używany w innych miejscach to o co nam chodzi
            var isUsed = _context.Stocks.Any(s => s.FishTankId == id);
            if (isUsed)
            {
                string errorMessage = "You cannot delete it because this fish is used in other tabs.";
                ModelState.AddModelError("ErrorMessage", errorMessage);
                return View("Delete", FishTanks);
                // Przekierowanie do widoku "Delete" z danymi environment, aby można było wyświetlić komunikat
            }


            // Normalna ścieżka, jeśli nie ma błędów
            _context.FishTanks.Remove(FishTanks);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index)); ;
        }

        private bool FishTankExists(int id)
        {
            return _context.FishTanks.Any(e => e.FishTankId == id);
        }
    }
}
