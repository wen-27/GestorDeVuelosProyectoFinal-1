using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Domain.ValueObject;

public sealed class PassengerReservationId
{
    public Guid Value { get; }
    private PassengerReservationId(Guid value) => Value = value;

    public static PassengerReservationId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El ID de la reserva del pasajero no es válido.");
        return new PassengerReservationId(value);
    }
}