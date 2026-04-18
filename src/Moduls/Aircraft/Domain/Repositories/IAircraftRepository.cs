using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;

public interface IAircraftRepository
{
    Task AddAsync(global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft aircraft, CancellationToken cancellationToken = default);

    Task<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft?> GetByIdAsync(AircraftId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft>> GetAllAsync(CancellationToken cancellationToken = default);
    Task UpdateAsync(global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft aircraft, CancellationToken cancellationToken = default);
    Task<bool> DeleteByIdAsync(AircraftId id, CancellationToken cancellationToken = default);

}
