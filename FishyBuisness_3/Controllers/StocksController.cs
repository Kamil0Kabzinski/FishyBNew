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
    public class StocksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StocksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Stocks.Include(s => s.Environment).Include(s => s.Fish).Include(s => s.FishTank);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Environment)
                .Include(s => s.Fish)
                .Include(s => s.FishTank)
                .FirstOrDefaultAsync(m => m.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name");
            ViewData["FishId"] = new SelectList(_context.Fish, "FishId", "FishName");
            ViewData["FishTankId"] = new SelectList(_context.FishTanks, "FishTankId", "Name");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockId,FishId,FishTankId,Quantity,EnvironmentId")] Stock stock)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stock);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", stock.EnvironmentId);
            ViewData["FishId"] = new SelectList(_context.Fish, "FishId", "FishName", stock.FishId);
            ViewData["FishTankId"] = new SelectList(_context.FishTanks, "FishTankId", "Name", stock.FishTankId);
            return View(stock);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", stock.EnvironmentId);
            ViewData["FishId"] = new SelectList(_context.Fish, "FishId", "FishName", stock.FishId);
            ViewData["FishTankId"] = new SelectList(_context.FishTanks, "FishTankId", "Name", stock.FishTankId);
            return View(stock);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockId,FishId,FishTankId,Quantity,EnvironmentId")] Stock stock)
        {
            if (id != stock.StockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stock);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockExists(stock.StockId))
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
            ViewData["EnvironmentId"] = new SelectList(_context.Environments, "EnvironmentId", "Name", stock.EnvironmentId);
            ViewData["FishId"] = new SelectList(_context.Fish, "FishId", "FishName", stock.FishId);
            ViewData["FishTankId"] = new SelectList(_context.FishTanks, "FishTankId", "Name", stock.FishTankId);
            return View(stock);
        }

        // GET: Stocks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stock = await _context.Stocks
                .Include(s => s.Environment)
                .Include(s => s.Fish)
                .Include(s => s.FishTank)
                .FirstOrDefaultAsync(m => m.StockId == id);
            if (stock == null)
            {
                return NotFound();
            }

            return View(stock);
        }

        // POST: Stocks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockExists(int id)
        {
            return _context.Stocks.Any(e => e.StockId == id);
        }
     
    }


}
