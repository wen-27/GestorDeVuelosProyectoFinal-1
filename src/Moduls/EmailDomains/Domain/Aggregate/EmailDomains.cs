using System;
using GestorDeVuelosProyectoFinal.Moduls.EmailDomains.Domain.ValueObject; // <--- Revisa esta línea letra por letra

namespace GestorDeVuelosProyectoFinal.Moduls.EmailDomains.Domain.Aggregate; // <--- Revisa esta línea letra por letra

public sealed class EmailDomain
{
    public EmailDomainsId Id { get; private set; } = null!;
    public EmailDomainName Name { get; private set; } = null!;

    private EmailDomain() { }

    private EmailDomain(EmailDomainsId id, EmailDomainName name)
    {
        Id = id;
        Name = name;
    }

    public static EmailDomain Create(Guid id, string name)
    {
        return new EmailDomain(
            EmailDomainsId.Create(id),
            EmailDomainName.Create(name)
        );
    }
}