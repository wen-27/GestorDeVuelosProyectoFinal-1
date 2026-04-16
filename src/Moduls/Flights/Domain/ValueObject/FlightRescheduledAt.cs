using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightRescheduledAt
{
    public DateTime Value { get; }

    private FlightRescheduledAt(DateTime value) => Value = value;

    public static FlightRescheduledAt Create(DateTime value)
    {   // verifica que la fecha no venga vacia
        if (value == DateTime.MinValue)
            throw new ArgumentException("La fecha de reemplazo del vuelo no es válida", nameof(value));

        // la reemplazo no puede ser en el pasado
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La fecha de reemplazo del vuelo no puede ser en el pasado", nameof(value));

        return new FlightRescheduledAt(value);
    }
}
