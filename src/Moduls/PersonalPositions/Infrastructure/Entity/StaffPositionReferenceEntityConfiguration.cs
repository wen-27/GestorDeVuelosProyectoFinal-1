using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Infrastructure.Persistence.Entities;

public sealed class StaffPositionReferenceEntityConfiguration : IEntityTypeConfiguration<StaffPositionReferenceEntity>
{
    public void Configure(EntityTypeBuilder<StaffPositionReferenceEntity> builder)
    {
        builder.ToTable("staff");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.PositionId)
            .HasColumnName("position_id");
    }
}
