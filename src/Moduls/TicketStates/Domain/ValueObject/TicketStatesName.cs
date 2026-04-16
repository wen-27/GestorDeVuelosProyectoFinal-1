using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Domain.ValueObject;

public sealed class TicketStatesName
{
    public string Value { get; }

    private TicketStatesName(string value) => Value = value;

    public static TicketStatesName Create(string value)
    {
        if (value.Length > 50)
            throw new ArgumentException("El nombre del estado de ticket no puede superar los 50 caracteres", nameof(value));

        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del estado de ticket no es válido", nameof(value));

        return new TicketStatesName(value);
    }
}
