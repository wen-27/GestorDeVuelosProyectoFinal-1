using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.ReservationStateTransitions.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.ReservationStateTransitions.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.ReservationStateTransitions.Domain.Repositories;

public interface IReservationStateTransitionsRepository
{
    Task<ReservationStateTransition?> GetByIdAsync(ReservationStateTransitionsId id);
    
    // Búsqueda por la combinación UNIQUE de las FKs (origin y destination)
    Task<ReservationStateTransition?> GetByTransitionAsync(ReserveStateId originId, ReserveStateId destinationId);

    Task<IEnumerable<ReservationStateTransition>> GetByOriginAsync(ReserveStateId originId);

    Task SaveAsync(ReservationStateTransition transition);
    Task DeleteAsync(ReservationStateTransitionsId id);
}