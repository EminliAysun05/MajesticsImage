using MajesticAdminPanelTask.DataAccesLayer;
using MajesticAdminPanelTask.DataAccesLayer.Entities;
using MajesticAdminPanelTask.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PB303Fashion.DataAccessLayer.Entities;

namespace MajesticAdminPanelTask.Areas.AdminPanel.Controllers
{
    public class HomePageImageController : AdminController
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomePageImageController(IWebHostEnvironment webHostEnvironment, AppDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }

       

        public async Task<IActionResult> Index()
        {

            var backgroundImages = await _dbContext.homeBackgroundImages.ToListAsync();

            return View(backgroundImages);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HomeBackgroundImages homeBackgroundImages)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!homeBackgroundImages.ImageFile.IsImage())
            {
                ModelState.AddModelError("ImageFile", "Yalnız şəkil formatında fayl seçməlisiniz.");

                return View();
            }

            if (!homeBackgroundImages.ImageFile.IsAllowedSize(1))
            {
                ModelState.AddModelError("ImageFile", " şəkil olcusu max 1 mb olmalidir.");

                return View();
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "images", "fashion","banner");

            var imageName = await homeBackgroundImages.ImageFile.GenerateFileAsync(path);

            homeBackgroundImages.ImageUrl = imageName;//bazada adini saxlayiriq seklin

            await _dbContext.homeBackgroundImages.AddAsync(homeBackgroundImages);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
