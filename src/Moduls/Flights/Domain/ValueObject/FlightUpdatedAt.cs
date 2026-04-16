using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightUpdatedAt
{
    public DateTime Value { get; }
    private FlightUpdatedAt(DateTime value) => Value = value;
    public static FlightUpdatedAt Create(DateTime value)
    {   // verifica que la fecha no venga vacia
        if (value == DateTime.MinValue)
            throw new ArgumentException("La fecha de actualización del vuelo no es válida", nameof(value));

        // la actualización no puede ser en el pasado
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La fecha de actualización del vuelo no puede ser en el pasado", nameof(value));

        return new FlightUpdatedAt(value);
    }
}
