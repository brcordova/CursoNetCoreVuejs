using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class LoginViewModel
    {
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "")]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
