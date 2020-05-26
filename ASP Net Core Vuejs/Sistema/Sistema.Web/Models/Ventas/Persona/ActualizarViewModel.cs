using System.ComponentModel.DataAnnotations;

namespace Sistema.Web.Models.Ventas.Persona
{
    public class ActualizarViewModel
    {
        [Key]
        public int idpersona { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El tipo de persona debe contener entre 3 y 20 caracteres.")]
        public string tipo_persona { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe contener entre 3 y 100 caracteres.")]
        public string nombre { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "El tipo de documento debe contener entre 3 y 20 caracteres.")]
        public string tipo_documento { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "El número de documento debe contener entre 3 y 20 caracteres.")]
        public string num_documento { get; set; }

        [StringLength(70, MinimumLength = 3, ErrorMessage = "La dirección debe contener entre 3 y 70 caracteres.")]
        public string direccion { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "El teléfono debe contener entre 3 y 20 caracteres.")]
        public string telefono { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Favor de capturar un correo electrónico válido.")]
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El correo electrónico debe contener entre 3 y 50 caracteres")]
        public string email { get; set; }
    }
}
