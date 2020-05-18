using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sistema.Entidades.Almacen;

namespace Sistema.Datos.Mapping.Almacen
{
    public class ArticuloMap : IEntityTypeConfiguration<Articulo> 
    {
        public void Configure(EntityTypeBuilder<Articulo> builder)
        {
            builder.ToTable("articulo")
                .HasKey(a => a.idarticulo);
            builder.Property(a => a.codigo)
                .HasMaxLength(50);
            builder.Property(a => a.nombre)
                .HasMaxLength(100);
            builder.Property(a => a.descripcion)
                .HasMaxLength(256);
        }
    }
}