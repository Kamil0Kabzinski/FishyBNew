using FishyBuisness_3.Data;
using FishyBuisness_3.Models;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FishyBuisness_3.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IHtmlLocalizer<HomeController> _localizer;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IHtmlLocalizer<HomeController> localizer, ApplicationDbContext context)
		{
			_logger = logger;
			_localizer = localizer;
			_context = context;
		}

        public async Task<IActionResult> Index()
        {
            var groupedStocks = _context.Stocks
                .Include(s => s.Environment)
                .Include(s => s.Fish)
                .Include(s => s.FishTank)
                .GroupBy(s => s.FishTankId);
			ViewData["FishDescription"] = _context.Fish;
            ViewData["Spieces"] = _context.Fish;
            ViewData["Price"] = _context.Fish;
            ViewData["Environment"] = _context.Environments;
            ViewData["FishTank"] = _context.FishTanks;
            return View("Index", await groupedStocks.ToListAsync());
        }


        [HttpPost]
		 public IActionResult setLanguage(string culture, string returnUrl)
		{
			Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
				new CookieOptions { Expires = DateTimeOffset.UtcNow.AddDays(1)
				});

			return LocalRedirect(returnUrl);

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
}
