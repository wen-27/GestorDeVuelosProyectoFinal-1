using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightsId
{
    public Guid Value { get; }

    private FlightsId(Guid value) => Value = value;

    public static FlightsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del vuelo no es válido", nameof(value));

        return new FlightsId(value);
    }
}
