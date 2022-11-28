using System.ComponentModel.DataAnnotations;

namespace CoreProject.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<Product> Products { get; set;}



    }
}
