using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

public sealed class FlightEstimatedArrivalTime
{
    public DateTime Value { get; }

    private FlightEstimatedArrivalTime(DateTime value) => Value = value;

    public static FlightEstimatedArrivalTime Create(DateTime value)
    {   // verifica que la fecha no venga vacia
        if (value == DateTime.MinValue)
            throw new ArgumentException("La hora de llegada estimada no es válida", nameof(value));
        
        // la llegada no puede ser en el pasado
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La hora de salida no puede ser en el pasado", nameof(value));

        return new FlightEstimatedArrivalTime(value);
    }
}
