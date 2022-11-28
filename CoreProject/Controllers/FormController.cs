using CoreProject.Data;
using CoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CoreProject.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private readonly AppDbContext _db;
        public FormController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task <IActionResult> Index(long id=1)
        {
            ViewBag.Categories = new SelectList(_db.Categories, "Id", "Name");
            return View(await _db.Products.Include(p=>p.Category).FirstAsync(p=>p.Id==id));
        }

        [HttpPost]
        public IActionResult SubmitForm(string name, decimal price)
        {
            TempData["name param"] = name;
            TempData["price param"] = price.ToString();


            return RedirectToAction("Results");
        }
        [HttpGet]
        public IActionResult Results()
        {
            return View();
        }

        [HttpGet]
        public string Header([FromHeader(Name ="Accept-Language")] string accept)
        {
            return $"Header:{accept}";
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public Product Body([FromBody] Product model)
        {
            return model;
        }
    }
}
