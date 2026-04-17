using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.UseCases;

public sealed class DeleteAircraftModelsUseCase
{
    private readonly IAircraftModelsRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
     public DeleteAircraftModelsUseCase(IAircraftModelsRepository repository, IUnitOfWork unitOfWork)
    {
        _repository  = repository;
        _unitOfWork  = unitOfWork;
    }

    public async Task<bool> ExecuteAsync(int id, CancellationToken cancellationToken = default)
    {
        var aircraftModelId = AircraftModelId.Create(id);

        var existing = await _repository.FindByIdAsync(aircraftModelId, cancellationToken)
            ?? throw new KeyNotFoundException($"AircraftModel con ID '{id}' no encontrado.");

        var deleted = await _repository.DeleteByIdAsync(aircraftModelId, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return deleted;
    }
}