using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.UseCases;

public class GetAllAircraftUseCase
{
    private readonly IAircraftRepository _aircraftRepository;

    public GetAllAircraftUseCase(IAircraftRepository aircraftRepository)
    {
        _aircraftRepository = aircraftRepository;
    }

    public async Task<IReadOnlyCollection<Domain.Aggregate.Aircraft>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return await _aircraftRepository.GetAllAsync(cancellationToken);
    }
}
