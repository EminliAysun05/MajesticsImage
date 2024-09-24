using MajesticAdminPanelTask.DataAccesLayer.Entities;
using Microsoft.EntityFrameworkCore;
using PB303Fashion.DataAccessLayer.Entities;

namespace MajesticAdminPanelTask.DataAccesLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<HomeBackgroundImages> homeBackgroundImages { get; set; }
        public DbSet<BackgroundImages> BackgroundImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Footer> Footers { get; set; }
    }
}
