using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PB303Fashion.DataAccessLayer.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public string? ImageUrl {  get; set; }
        [NotMapped] //baza modeli bunu nezere almasin deye
        public IFormFile ImageFile { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
