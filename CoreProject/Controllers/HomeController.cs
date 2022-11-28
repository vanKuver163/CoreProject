using CoreProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }


    
        public async Task<IActionResult> Index(long id = 1)
        {
            return View(await _db.Products.FindAsync(id));
        }

        public IActionResult Common(long id)
        {
            return View("/Views/Shared/Common.cshtml");
        }

        public async Task<IActionResult> List()
        {
            ViewBag.AveragePrice = await _db.Products.AverageAsync(p => p.Price);
            return View(await _db.Products.ToListAsync());
        }

        public IActionResult Redirect()
        {
            TempData["value"] = "TempData value";
            return RedirectToAction("Index", new { id = 1 });
        }

        public IActionResult Html()
        {           
            return View((object)"This is a <h3><i>string</i></h3>");
        }
    }
}
