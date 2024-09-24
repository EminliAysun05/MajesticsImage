using Microsoft.AspNetCore.Mvc;

namespace MajesticAdminPanelTask.Areas.AdminPanel.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
