using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Infrastructure.Persistence.Entities;

namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Infrastructure.Persistence.Entities;

public sealed class CityEntityConfiguration : IEntityTypeConfiguration<CityEntity>
{
    public void Configure(EntityTypeBuilder<CityEntity> builder)
    {
        builder.ToTable("cities");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.RegionId)
            .HasColumnName("region_id")
            .IsRequired();
            
        // Si quieres que MySQL borre las ciudades si se borra la región:
        builder.HasOne<GestorDeVuelosProyectoFinal.Moduls.Regions.Infrastructure.Persistence.Entities.RegionEntity>()
            .WithMany()
            .HasForeignKey(c => c.RegionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}