using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts; // Para el IUnitOfWork

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.UseCases;

public sealed class UpdateAircraftModelsUseCase
{
    private readonly IAircraftModelsRepository _aircraftModelsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAircraftModelsUseCase(
        IAircraftModelsRepository aircraftModelsRepository, 
        IUnitOfWork unitOfWork)
    {
        _aircraftModelsRepository = aircraftModelsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<AircraftModel> ExecuteAsync(
        int id, 
        string name, 
        int maxCapacity, 
        decimal? weight, 
        decimal? fuelConsumption, 
        int? cruiseSpeed, 
        int? cruiseAltitude, 
        CancellationToken cancellationToken = default)
    {

        var aircraftModelId = AircraftModelId.Create(id);

        var existingAircraftModel = await _aircraftModelsRepository.FindByIdAsync(aircraftModelId, cancellationToken);

        if (existingAircraftModel is null)
        {
            throw new KeyNotFoundException($"AircraftModel with id '{id}' was not found.");
        }

        var updatedAircraftModel = AircraftModel.Create(
            id, 
            name, 
            maxCapacity, 
            weight, 
            fuelConsumption, 
            cruiseSpeed, 
            cruiseAltitude
        );

        await _aircraftModelsRepository.UpdateAsync(updatedAircraftModel, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return updatedAircraftModel;
    }
}