using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

public sealed class ReserveTotalAmount
{
    public decimal Value { get; }

    private ReserveTotalAmount(decimal value) => Value = value;

    public static ReserveTotalAmount Create(decimal value)
    {
        if (value < 0)
            throw new ArgumentException("El monto total de la reserva no es válido", nameof(value));

        return new ReserveTotalAmount(value);
    }
}
