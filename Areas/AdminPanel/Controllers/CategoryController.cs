using MajesticAdminPanelTask.DataAccesLayer;
using MajesticAdminPanelTask.Extension;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using PB303Fashion.DataAccessLayer.Entities;

namespace MajesticAdminPanelTask.Areas.AdminPanel.Controllers;
[Area("AdminPanel")]
public class CategoryController : Controller
{
    private readonly AppDbContext _dbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public CategoryController(AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
    {
        _dbContext = dbContext;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _dbContext.Categories.ToListAsync();
        return View(categories);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]

  

    //[HttpPost]
    //public async Task<IActionResult> Create(string name, string description)
    //{
    //    return Content(name + " " + description);
    //    return RedirectToAction(nameof(Index));
    //}

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }

        if (!category.ImageFile.IsImage())
        {
            ModelState.AddModelError("ImageFile", "Yalnız şəkil formatında fayl seçməlisiniz.");

            return View();
        }

        if(category.ImageFile.IsAllowedSize(1))
        {
            ModelState.AddModelError("ImageFile", " şəkil olcusu max 1 mb olmalidir.");

            return View();
        }

        var path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "svg", "fashion");
        
        var imageName = await category.ImageFile.GenerateFileAsync(path);

        category.ImageUrl = imageName;//bazada adini saxlayiriq seklin

        await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();   

        return RedirectToAction(nameof(Index));
    }
}
