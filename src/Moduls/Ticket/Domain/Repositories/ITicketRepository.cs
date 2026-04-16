using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Domain.Repositories;

public interface ITicketRepository
{
    Task<Aggregate.Ticket?> GetByIdAsync(TicketId id);
    Task<IEnumerable<Aggregate.Ticket>> GetAllAsync();
    Task SaveAsync(Aggregate.Ticket ticket);
    Task DeleteAsync(TicketId id);
}
