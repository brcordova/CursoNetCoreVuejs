using Sistema.Entidades.Usuarios;
using Sistema.Entidades.Ventas;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sistema.Entidades.Almacen
{
	public class Ingreso
    {
		[Key]
		public int idingreso { get; set; }

		[ForeignKey("persona")]
		[Required]
		public int idproveedor { get; set; }

		[ForeignKey("usuario")]
		[Required]
		public int idusuario { get; set; }

		[StringLength(20, MinimumLength = 3, ErrorMessage = "")]
		[Required]
		public string tipo_comprobante { get; set; }

		[StringLength(7, MinimumLength = 3, ErrorMessage = "")]
		[Required]
		public string serie_comprobante { get; set; }

		[StringLength(10, MinimumLength = 3, ErrorMessage = "")]
		[Required]
		public string num_comprobante { get; set; }

		[Required]
		public DateTime fecha_hora { get; set; }

		[Required]
		public decimal impuesto { get; set; }

		[Required]
		public decimal total { get; set; }

		[Required]
		public string estado { get; set; }

		public ICollection<DetalleIngreso> detalles { get; set; }
		public Usuario usuario { get; set; }
		public Persona persona { get; set; }
	}
}
