using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightDepartureTime
{
    public DateTime Value { get; }

    private FlightDepartureTime(DateTime value) => Value = value;

    public static FlightDepartureTime Create(DateTime value)
    {   // verifica que la fecha no venga vacia
        if (value == DateTime.MinValue)
            throw new ArgumentException("La hora de salida del vuelo no es válida", nameof(value));

        // la salida no puede ser en el pasado
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La hora de salida no puede ser en el pasado", nameof(value));

        return new FlightDepartureTime(value);
    }
}
    
