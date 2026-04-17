using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.ValueObject;

public sealed class PersonalId
{
    public int Value { get; }
    private PersonalId(int value) => Value = value;
    public static PersonalId Create(int value)
    {
        if (value == int.Empty) throw new ArgumentException("El ID de personal no es válido.");
        return new PersonalId(value);
    }
}