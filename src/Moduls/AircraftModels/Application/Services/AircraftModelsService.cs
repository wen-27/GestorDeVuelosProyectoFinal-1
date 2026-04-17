using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.UseCases;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.Services;

public sealed class AircraftModelService : IAircraftModelsService
{
    private readonly GetAllAircraftModels _getAll;
    private readonly GetAircraftModelByIdUseCase _getById;   // ← use case dedicado
    private readonly CreateAircraftModelsUseCase _create;
    private readonly UpdateAircraftModelsUseCase _update;
    private readonly DeleteAircraftModelsUseCase _delete;

    public AircraftModelService(
        GetAllAircraftModels getAll,
        GetAircraftModelByIdUseCase getById,
        CreateAircraftModelsUseCase create,
        UpdateAircraftModelsUseCase update,
        DeleteAircraftModelsUseCase delete)
    {
        _getAll  = getAll;
        _getById = getById;
        _create  = create;
        _update  = update;
        _delete  = delete;
    }

    public Task<IReadOnlyCollection<AircraftModel>> GetAllAsync(CancellationToken ct = default)
        => _getAll.ExecuteAsync(ct);

    public Task<AircraftModel?> GetByIdAsync(int id, CancellationToken ct = default)
        => _getById.ExecuteAsync(id, ct);

    public Task<AircraftModel> CreateAsync(
        int id, string name, int maxCapacity,
        decimal? weight, decimal? fuelConsumption,
        int? cruiseSpeed, int? cruiseAltitude,
        CancellationToken ct = default)
        => _create.ExecuteAsync(id, name, maxCapacity, weight, fuelConsumption, cruiseSpeed, cruiseAltitude, ct);

    public Task<AircraftModel> UpdateAsync(
        int id, string name, int maxCapacity,
        decimal? weight, decimal? fuelConsumption,
        int? cruiseSpeed, int? cruiseAltitude,
        CancellationToken ct = default)
        => _update.ExecuteAsync(id, name, maxCapacity, weight, fuelConsumption, cruiseSpeed, cruiseAltitude, ct);

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => _delete.ExecuteAsync(id, ct);
}