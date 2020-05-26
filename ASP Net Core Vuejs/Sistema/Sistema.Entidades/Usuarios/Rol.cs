using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Usuarios
{
    public class Rol
    {
        [Key]
        public int idRol { get; set; }
        
        [Required]
        [StringLength(30,MinimumLength =3, ErrorMessage ="El nombre debe de tener mínimo 3 caracteres y máximo 30")]
        public string nombre { get; set; }

        [StringLength(256)]
        public string descripcion { get; set; }

        public bool condicion { get; set; }

        public ICollection<Usuario> Usuarios { get; set; }
    }
}
