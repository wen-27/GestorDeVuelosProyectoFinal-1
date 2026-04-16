using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightsCreatedAt
{
    public DateTime Value { get; }

    private FlightsCreatedAt(DateTime value) => Value = value;

    public static FlightsCreatedAt Create(DateTime value)
    {   // verifica que la fecha no venga vacia
        if (value == DateTime.MinValue)
            throw new ArgumentException("La fecha de creación del vuelo no es válida", nameof(value));

        // la creación no puede ser en el pasado
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La fecha de creación del vuelo no puede ser en el pasado", nameof(value));

        return new FlightsCreatedAt(value);
    }
}
