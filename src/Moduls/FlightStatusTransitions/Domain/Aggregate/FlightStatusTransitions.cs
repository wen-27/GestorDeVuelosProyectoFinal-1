using System;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightStatusTransitions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightStatusTransitions.Domain.Aggregate;

public sealed class FlightStatusTransitions
{
    public FlightStatusTransitionsId Id { get; private set; } = null!;

    private FlightStatusTransitions() { }

    private FlightStatusTransitions(
        FlightStatusTransitionsId id)
    {
        Id = id;
    }

    public static FlightStatusTransitions Create(
        Guid id)
    {
        return new FlightStatusTransitions(
            FlightStatusTransitionsId.Create(id)
        );
    }
}
