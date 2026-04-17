using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;


namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Repositories;

public interface IAircraftModelsRepository
{
    Task AddAsync(AircraftModel model, CancellationToken cancellationToken = default);

    Task<AircraftModel?> FindByIdAsync(AircraftModelId id, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<AircraftModel>> FindAllAsync(CancellationToken cancellationToken = default);

    Task UpdateAsync(AircraftModel model, CancellationToken cancellationToken = default);

    Task<bool> DeleteByIdAsync(AircraftModelId id, CancellationToken cancellationToken = default);
}