using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Domain.ValueObject;

public sealed class TicketId
{
    public Guid Value { get; }

    private TicketId(Guid value) => Value = value;

    public static TicketId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del ticket no es válido", nameof(value));

        return new TicketId(value);
    }
}
