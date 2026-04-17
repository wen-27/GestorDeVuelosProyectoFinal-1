using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;


namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.UseCases;

public sealed class CreateAircraftModelsUseCase
{
    private readonly IAircraftModelsRepository _aircraftModelsRepository;
    private readonly IUnitOfWork _unitOfWork;
    public CreateAircraftModelsUseCase(
        IAircraftModelsRepository aircraftModelsRepository, IUnitOfWork unitOfWork)
    {
        _aircraftModelsRepository = aircraftModelsRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<AircraftModel> ExecuteAsync(int id, string name, int maxCapacity, decimal? weight, decimal? fuelConsumption, int? cruiseSpeed, int? cruiseAltitude, CancellationToken cancellationToken = default)
    {
        var aircraftModelId = AircraftModelId.Create(id);
        var existingAircraftModel = await _aircraftModelsRepository.FindByIdAsync(aircraftModelId, cancellationToken);

        if (existingAircraftModel is not null)
        {
            throw new InvalidOperationException($"AircraftModel with id '{aircraftModelId}' already exists.");
        }
        var aircraftModel = AircraftModel.Create(id, name, maxCapacity, weight, fuelConsumption, cruiseSpeed, cruiseAltitude);
        await _aircraftModelsRepository.AddAsync(aircraftModel, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return aircraftModel;
    }
}