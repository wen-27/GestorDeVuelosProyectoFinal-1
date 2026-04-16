using System;

namespace GestorDeVuelosProyectoFinal.Moduls.ReservationStateTransitions.Domain.ValueObject;

public sealed class ReservationStateTransitionsId
{
    public Guid Value { get; }
    private ReservationStateTransitionsId(Guid value) => Value = value;

    public static ReservationStateTransitionsId Create(Guid value)
    {
        if (value == Guid.Empty) 
            throw new ArgumentException("El ID de transición de estado de reserva no es válido.");
        return new ReservationStateTransitionsId(value);
    }
}