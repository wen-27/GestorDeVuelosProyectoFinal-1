using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.ValueObject;

public sealed class PersonalPositionsId 
{
    public Guid Value { get; }

    private PersonalPositionsId(Guid value) => Value = value;

    public static PersonalPositionsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del personal no es válido", nameof(value));

        return new PersonalPositionsId(value);
    }
}