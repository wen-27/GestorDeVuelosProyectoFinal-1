using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

public sealed class ReverseId
{
    public Guid Value { get; }

    private ReverseId(Guid value) => Value = value;

    public static ReverseId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la reserva no es válido", nameof(value));

        return new ReverseId(value);
    }
}
