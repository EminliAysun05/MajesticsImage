using System.ComponentModel.DataAnnotations;

namespace MajesticAdminPanelTask.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
