﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Entidades.Almacen
{
    public class Categoria
    {
        [Key]
        public int idcategoria { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength =3, ErrorMessage ="El nombre no debe de contener más de 50 caracteres, ni menos de 3 caracteres.")]
        public string nombre { get; set; }
        
        [StringLength(256)]
        public string descripcion { get; set; }
        public bool condicion { get; set; }

        public ICollection<Articulo> articulos { get; set; }
    }
}
