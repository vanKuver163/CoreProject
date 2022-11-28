using CoreProject.Data;
using CoreProject.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public CategoriesController(AppDbContext db)
        {
            _db = db;
        }


        //api/categories/id
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml")]
        public async Task<Category> GetProduct(long id)
        {
            Category category = await _db.Categories.Include(c => c.Products).FirstAsync(c=>c.Id==id);
            if(category.Products != null)
            {
                foreach(Product product in category.Products)
                {
                    product.Category = null;
                }
            }
            return category;
        }

        //api/categories/id
        [HttpPatch("{id}")]
        public async Task<Category> PatchProduct(long id, JsonPatchDocument<Category> patchDoc)
        {
            Category category = await _db.Categories.FindAsync(id);
           
            if(category != null)
            {
                patchDoc.ApplyTo(category);
                await _db.SaveChangesAsync();
            }

            return category;
        }


    }
}
