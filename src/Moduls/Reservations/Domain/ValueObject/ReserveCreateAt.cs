using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

public class ReserveCreateAt
{
    public DateTime Value { get; }

    private ReserveCreateAt(DateTime value) => Value = value;

    public static ReserveCreateAt Create(DateTime value)
    {
        if (value == DateTime.MinValue)
            throw new ArgumentException("La fecha de creación de la reserva no es válida", nameof(value));

        // la reserva no puede ser en el pasado
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La fecha de creación de la reserva no puede ser en el pasado", nameof(value));

        return new ReserveCreateAt(value);
    }
}
