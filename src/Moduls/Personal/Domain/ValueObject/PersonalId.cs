using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.ValueObject;

public sealed class PersonalId
{
    public Guid Value { get; }
    private PersonalId(Guid value) => Value = value;
    public static PersonalId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID de personal no es válido.");
        return new PersonalId(value);
    }
}