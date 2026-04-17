using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.UseCases;

public sealed class GetAllAircraftModels
{
    private readonly IAircraftModelsRepository _aircraftModelsRepository;

    public GetAllAircraftModels(IAircraftModelsRepository aircraftModelsRepository)
    {
        _aircraftModelsRepository = aircraftModelsRepository;
    }

    public Task<IReadOnlyCollection<AircraftModel>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return _aircraftModelsRepository.FindAllAsync(cancellationToken);
    }
}