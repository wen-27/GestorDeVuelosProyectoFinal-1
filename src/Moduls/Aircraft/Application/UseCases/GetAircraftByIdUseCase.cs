using GestorDeVuelosProyectoFinal.Moduls.Aircraft.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.UseCases;

public sealed class GetAircraftByIdUseCase
{
    private readonly IAircraftRepository _repository;

    public GetAircraftByIdUseCase(IAircraftRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Aggregate.Aircraft?> ExecuteAsync(AircraftId id, CancellationToken cancellationToken = default)
    {
        return await _repository.GetByIdAsync(id, cancellationToken);
    }
}
