using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.ValueObject;

public sealed record PersonEmailsId
{
    public Guid Value { get; }
    private PersonEmailsId(Guid value) => Value = value;

    public static PersonEmailsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El ID del email de la persona no puede estar vacío.", nameof(value));

        return new PersonEmailsId(value);
    }
}