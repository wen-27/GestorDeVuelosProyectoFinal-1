using GestorDeVuelosProyectoFinal.Moduls.People.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.EmailDomains.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Infrastructure.Entity;

public sealed class PersonEmailEntityConfiguration : IEntityTypeConfiguration<PersonEmailEntity>
{
    public void Configure(EntityTypeBuilder<PersonEmailEntity> builder)
    {
        builder.ToTable("people_emails");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(x => x.PersonId)
            .HasColumnName("person_id")
            .IsRequired();

        builder.Property(x => x.EmailUser)
            .HasColumnName("email_user")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.EmailDomainId)
            .HasColumnName("email_domain_id")
            .IsRequired();

        builder.Property(x => x.IsPrimary)
            .HasColumnName("is_primary")
            .HasDefaultValue(false)
            .IsRequired();

        // Regla útil: evita duplicar exactamente el mismo email completo
        // (user + domain) para la misma persona.
        builder.HasIndex(x => new { x.PersonId, x.EmailUser, x.EmailDomainId })
            .IsUnique();

        // Relación 1:N persons -> people_emails
        // Una persona puede tener muchos emails.
        // FK física: person_id -> persons.id
        builder.HasOne<PersonEntity>()
            .WithMany()
            .HasForeignKey(x => x.PersonId)
            .HasConstraintName("fk_people_emails_person")
            .OnDelete(DeleteBehavior.Restrict);

        // Relación 1:N email_domains -> people_emails
        // Un dominio puede usarse en muchos emails de personas.
        // FK física: email_domain_id -> email_domains.id
        builder.HasOne<EmailDomainsEntity>()
            .WithMany()
            .HasForeignKey(x => x.EmailDomainId)
            .HasConstraintName("fk_people_emails_email_domain")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
