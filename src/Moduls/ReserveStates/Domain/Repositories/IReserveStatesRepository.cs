using System;
using GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.Repositories;

public interface IReserveStatesRepository
{
    Task<Aggregate.ReserveStates?> GetByIdAsync(ReserveStateId id);
    Task<IEnumerable<Aggregate.ReserveStates>> GetAllAsync();
    Task SaveAsync(Aggregate.ReserveStates reserveStates);
    Task DeleteAsync(ReserveStateId id);
}
