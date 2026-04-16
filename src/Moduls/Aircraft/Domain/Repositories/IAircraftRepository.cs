using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;

public interface IAircraftRepository
{
    Task<Aggregate.Aircraft?> GetByIdAsync(AircraftId id);
    Task<IEnumerable<Aggregate.Aircraft>> GetAllAsync();
    Task SaveAsync(Aggregate.Aircraft aircraft);
    Task DeleteAsync(AircraftId id);
}
