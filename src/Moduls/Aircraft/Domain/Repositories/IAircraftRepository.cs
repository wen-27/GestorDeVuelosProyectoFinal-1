using GestorDeVuelosProyectoFinal.Moduls.Aircraft.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;

public interface IAircraftRepository
{
    Task AddAsync(GestorDeVuelosProyectoFinal.Moduls.Aircraft.Domain.Aggregate.Aircraft aircraft, CancellationToken cancellationToken = default);

    Task<GestorDeVuelosProyectoFinal.Moduls.Aircraft.Domain.Aggregate.Aircraft?> GetByIdAsync(AircraftId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<GestorDeVuelosProyectoFinal.Moduls.Aircraft.Domain.Aggregate.Aircraft>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(GestorDeVuelosProyectoFinal.Moduls.Aircraft.Domain.Aggregate.Aircraft aircraft, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(AircraftId id, CancellationToken cancellationToken = default);

}
