using PB303Fashion.DataAccessLayer.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace MajesticAdminPanelTask.DataAccesLayer.Entities
{
    public class BackgroundImages:Entity
    {

        public string? ImageUrl { get; set; }
        [NotMapped] //baza modeli bunu nezere almasin deye
        public IFormFile ImageFile { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string SubTitle { get; set; }
    }
}
