using CoreProject.Data;
using CoreProject.Models.ViewModels;
using CoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreProject.Controllers
{
    public class CrudController : Controller
    {

        private readonly AppDbContext _db;
        public CrudController(AppDbContext db)
        {
            _db = db;
        }


        public IActionResult Index() => View(_db.Products.Include(p => p.Category));

        public async Task<IActionResult> Details(long id)
        {
            Product product = await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            ProductViewModel model = ViewModelFactory.Details(product);
            return View("ProductEditor", model);
        }
        public IActionResult Create() => View("ProductEditor", ViewModelFactory.Create(new Product(), _db.Categories));

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Product product)
        {

            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("ProductEditor", ViewModelFactory.Create(product, _db.Categories));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            Product product = await _db.Products.FindAsync(id);

            if (product != null)
            {
                ProductViewModel model = ViewModelFactory.Edit(product, _db.Categories);
                return View("ProductEditor", model);

            }
            return View("ProductEditor", ViewModelFactory.Create(new Product(), _db.Categories));

        }

        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Product product)
        {

            if (ModelState.IsValid)
            {
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View("ProductEditor", ViewModelFactory.Edit(product, _db.Categories));
        }


        [HttpGet]
        public async Task<IActionResult> Delete(Product product)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
