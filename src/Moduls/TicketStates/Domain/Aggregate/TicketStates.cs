using System;
using GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Domain.Aggregate;

public sealed class TicketStates
{
    public TicketStatesId Id { get; private set; } = null!;
    public TicketStatesName Name { get; private set; } = null!;

    private TicketStates() { }

    private TicketStates(
        TicketStatesId id,
        TicketStatesName name)
    {
        Id = id;
        Name = name;
    }

    public static TicketStates Create(
        Guid id,
        string name)
    {
        return new TicketStates(
            TicketStatesId.Create(id),
            TicketStatesName.Create(name)
        );
    }

    public void UpdateName(string newName)
    {
        // El Value Object TicketStatesName se encarga de validar (longitud, números, etc.)
        Name = TicketStatesName.Create(newName);
    }   
}
