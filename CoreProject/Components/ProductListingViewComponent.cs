using CoreProject.Data;
using CoreProject.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.EntityFrameworkCore;

namespace CoreProject.Components
{
    
    public class ProductListingViewComponent : ViewComponent
    {
        private AppDbContext _db;

        public IEnumerable<Product> Products;
        public ProductListingViewComponent(AppDbContext db)
        {
            _db = db;
        }

        public IViewComponentResult Invoke(string className = "primary")
        {
            ViewBag.Class = className;
            return View(_db.Products.Include(p=>p.Category).ToList());
        }

    }
}
