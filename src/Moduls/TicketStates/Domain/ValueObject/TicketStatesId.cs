using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Domain.ValueObject;

public sealed class TicketStatesId
{
    public Guid Value { get; }

    private TicketStatesId(Guid value) => Value = value;

    public static TicketStatesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del estado de ticket no es válido", nameof(value));

        return new TicketStatesId(value);
    }
}
