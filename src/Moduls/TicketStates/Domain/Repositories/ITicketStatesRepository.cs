using System;
using GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.TicketStates.Domain.Repositories;

public interface ITicketStatesRepository
{
    Task<Aggregate.TicketStates?> GetByIdAsync(TicketStatesId id);
    Task<IEnumerable<Aggregate.TicketStates>> GetAllAsync();
    Task SaveAsync(Aggregate.TicketStates ticketStates);
    Task DeleteAsync(TicketStatesId id);
}
