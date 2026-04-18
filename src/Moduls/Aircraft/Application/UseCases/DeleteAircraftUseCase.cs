using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.UseCases;

public sealed class DeleteAircraftUseCase
{
    private readonly IAircraftRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAircraftUseCase(IAircraftRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(AircraftId id, CancellationToken cancellationToken = default)
    {
        var aircraft = await _repository.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException($"Avión con id '{id}' no encontrado.");

        await _repository.DeleteByIdAsync(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
