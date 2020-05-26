using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Almacen
{
    public class DetalleIngreso
    {
        [Key]
        public int iddetalle_ingreso { get; set; }

        [Required]
        public int idingreso { get; set; }

        [Required]
        public int idarticulo { get; set; }

        [Required]
        public int cantidad { get; set; }

        [Required]
        public decimal precio { get; set; }

        public Ingreso ingreso { get; set; }

    }
}
