using Microsoft.AspNetCore.Identity;

namespace MajesticAdminPanelTask.DataAccesLayer.Entities
{
    public class AppUser:IdentityUser
    {
        public string Fullname { get; set; }
    }
}
