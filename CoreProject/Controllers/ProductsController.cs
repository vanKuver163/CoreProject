using CoreProject.Data;
using CoreProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }




        //api/products
        [HttpGet]
      public IAsyncEnumerable<Product> GetProducts()
        {
            return _db.Products.AsAsyncEnumerable();            
        }

        //api/products/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id)
        {
            Product product = await _db.Products.FindAsync(id);
            if (product == null) 
            {
                return NotFound();
            }
            return Ok(product);
        }

        //api/products
        [HttpPost]
        [Consumes("application/xml")]
        public async Task<IActionResult> SaveProduct(long id, [FromBody] Product product)
        {
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();
                return Ok(product);
         
            
        }

        //api/products
        [HttpPut]
        public void UpdateProduct(long id, Product product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
        }

        //api/products/id
        [HttpDelete("{id}")]
        public void DeleteProduct(long id)
        {

            _db.Products.Remove(new Product { Id = id});
            _db.SaveChanges();
        }

        //api/products/redirect
        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            // return Redirect("/api/products/1");
            //return RedirectToAction(nameof(GetProduct), new { Id = 1 });
            return RedirectToRoute(new
            {
                controller = "Products",
                action = "GetProduct",
                Id = 1
            });
        }

    }
}
