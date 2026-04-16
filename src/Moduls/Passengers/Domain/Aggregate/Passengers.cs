using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.Aggregate;

public sealed class Passengers
{
    public PassengersId Id { get; private set; } = null!;

    private Passengers() { }

    private Passengers(PassengersId id)
    {
        Id = id;
    }

    public static Passengers Create(Guid id)
    {
        return new Passengers(
            PassengersId.Create(id)
        );
    }
}
