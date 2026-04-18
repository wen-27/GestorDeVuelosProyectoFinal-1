using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Infrastructure.Persistence.Entities;

// --- ESTOS SON LOS USINGS QUE SOLUCIONAN LOS ERRORES ---
using GestorDeVuelosProyectoFinal.Moduls.StreetTypes.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Infrastructure.Persistence.Entities;
// -------------------------------------------------------

namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Infrastructure.Persistence.Entities;

public sealed class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
{
    public void Configure(EntityTypeBuilder<AddressEntity> builder)
    {
        // Nombre de la tabla según tu SQL: "direcciones"
        builder.ToTable("direcciones");

        // Clave Primaria
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd(); 

        // Mapeo según tu SQL: tipo_via_id
        builder.Property(x => x.StreetTypeId)
            .HasColumnName("tipo_via_id")
            .IsRequired();

        // Mapeo según tu SQL: nombre_via
        builder.Property(x => x.StreetName)
            .HasColumnName("nombre_via")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Number)
            .HasColumnName("numero")
            .HasMaxLength(20)
            .IsRequired(false); 

        builder.Property(x => x.Complement)
            .HasColumnName("complemento")
            .HasMaxLength(100)
            .IsRequired(false); 

        builder.Property(x => x.CityId)
            .HasColumnName("ciudad_id")
            .IsRequired();

        builder.Property(x => x.PostalCode)
            .HasColumnName("codigo_postal")
            .HasMaxLength(20)
            .IsRequired(false); 

        // --- Relaciones (Foreign Keys) ---

        // Ahora el compilador encontrará StreetTypeEntity gracias al using de arriba
        builder.HasOne<StreetTypeEntity>()
            .WithMany()
            .HasForeignKey(x => x.StreetTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        // Ahora el compilador encontrará CityEntity gracias al using de arriba
        builder.HasOne<CityEntity>()
            .WithMany()
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}