using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightStatusTransitions.Domain.ValueObject;

public sealed class FlightStatusTransitionsId
{
    public Guid Value { get; }

    private FlightStatusTransitionsId(Guid value) => Value = value;

    public static FlightStatusTransitionsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la transición de estado no es válido", nameof(value));

        return new FlightStatusTransitionsId(value);
    }
}
