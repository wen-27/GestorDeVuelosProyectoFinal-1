using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.ValueObject;

public sealed class ReserveStateId
{
    public Guid Value { get; }

    private ReserveStateId(Guid value) => Value = value;

    public static ReserveStateId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la reserva no es válido", nameof(value));

        return new ReserveStateId(value);
    }
}
