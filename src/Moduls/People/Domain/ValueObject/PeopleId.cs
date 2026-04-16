using System;

namespace GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject;

public sealed record PeopleId
{
    public Guid Value { get; }
    private PeopleId(Guid value) => Value = value;
    public static PeopleId Create(Guid value) => new(value == Guid.Empty ? Guid.NewGuid() : value);
}