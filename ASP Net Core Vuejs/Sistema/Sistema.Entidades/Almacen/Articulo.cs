using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
    public class Articulo
    {
        [Key]
        public int idarticulo { get; set; }

        [ForeignKey("categoria")]
        [Required]
        public int idcategoria { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "El nombre no debe de contener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string codigo { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre no debe de contener más de 100 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }

        [Required]
        public decimal precio_venta { get; set; }

        [Required]
        public int stock { get; set; }

        [StringLength(256)]
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        public Categoria categoria { get; set; }

        //[ForeignKey("IdCategoria")]
        //public virtual Categoria Categoria { get; set; }

    }
}
