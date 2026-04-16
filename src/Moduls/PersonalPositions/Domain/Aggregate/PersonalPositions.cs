using System;
using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.Aggregate;

public sealed class PersonalPosition
{
    public PersonalPositionsId Id { get; private set; } = null!;
    public PersonalPositionsName Name { get; private set; } = null!;

    private PersonalPosition() { }

    public static PersonalPosition Create(Guid id, string name)
    {
        return new PersonalPosition
        {
            Id = PersonalPositionsId.Create(id),
            Name = PersonalPositionsName.Create(name)
        };
    }
}