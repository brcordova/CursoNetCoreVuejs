using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Web.Models.Usuarios.Usuario
{
    public class CrearViewModel
    {
        [ForeignKey("rol")]
        [Required]
        public int idrol { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "")]
        public string nombre { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "")]
        public string tipo_documento { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "")]
        public string num_documento { get; set; }

        [StringLength(70, MinimumLength = 3, ErrorMessage = "")]
        public string direccion { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "")]
        public string telefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "")]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
