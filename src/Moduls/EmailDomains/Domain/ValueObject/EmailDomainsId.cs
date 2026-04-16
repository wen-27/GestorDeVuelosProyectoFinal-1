using System;

namespace GestorDeVuelosProyectoFinal.Moduls.EmailDomains.Domain.ValueObject;

public sealed class EmailDomainsId 
{
    public Guid Value { get; }

    private EmailDomainsId(Guid value) => Value = value;

    public static EmailDomainsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del dominio de correo no es válido", nameof(value));

        return new EmailDomainsId(value);
    }
}