using Sistema.Entidades.Almacen;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Usuarios
{
    public class Usuario
    {
        [Key]
        public int idusuario { get; set; }

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
        public byte[] password_hash { get; set; }

        [Required]
        public byte[] password_salt { get; set; }


        public bool condicion { get; set; }

        public Rol rol { get; set; }
        public ICollection<Ingreso> ingresos { get; set; }

    }
}
