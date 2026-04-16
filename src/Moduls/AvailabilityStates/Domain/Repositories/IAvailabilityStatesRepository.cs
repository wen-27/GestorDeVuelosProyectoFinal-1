using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.Repositories;

public interface IAvailabilityStatesRepository
{
    Task<AvailabilityState?> GetByIdAsync(AvailabilityStatesId id);
    
    // Aprovechamos el UNIQUE de tu tabla SQL para buscar por nombre (Disponible, Vacaciones, etc.)
    Task<AvailabilityState?> GetByNameAsync(AvailabilityStatesName name);

    Task<IEnumerable<AvailabilityState>> GetAllAsync();
    
    Task SaveAsync(AvailabilityState state);
    Task DeleteAsync(AvailabilityStatesId id);
}