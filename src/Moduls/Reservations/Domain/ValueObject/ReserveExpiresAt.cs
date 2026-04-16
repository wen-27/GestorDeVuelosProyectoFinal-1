using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

public sealed class ReserveExpiresAt
{
    public DateTime Value { get; }

    private ReserveExpiresAt(DateTime value) => Value = value;

    public static ReserveExpiresAt Create(DateTime value)
    {
        if (value == DateTime.MinValue)
            throw new ArgumentException("La fecha de vencimiento de la reserva no es válida", nameof(value));

        // la reserva no puede vencer en el pasado
        if (value < DateTime.UtcNow)
            throw new ArgumentException("La fecha de vencimiento de la reserva no puede ser en el pasado", nameof(value));

        return new ReserveExpiresAt(value);
    }
}
