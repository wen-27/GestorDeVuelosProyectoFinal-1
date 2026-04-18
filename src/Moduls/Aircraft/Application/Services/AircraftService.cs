using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.UseCases;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.Services;

public sealed class AircraftService : IAircraftService
{
    private readonly GetAllAircraftUseCase _getAll;
    private readonly GetAircraftByIdUseCase _getById;
    private readonly CreateAircraftUseCase _create;
    private readonly UpdtaeAircraftUseCase _update; // ← corregido nombre
    private readonly DeleteAircraftUseCase _delete;

    public AircraftService(
        GetAllAircraftUseCase getAll,
        GetAircraftByIdUseCase getById,
        CreateAircraftUseCase create,
        UpdtaeAircraftUseCase update, // ← corregido
        DeleteAircraftUseCase delete)
    {
        _getAll = getAll;
        _getById = getById;
        _create = create;
        _update = update;
        _delete = delete;
    }

    public Task<IReadOnlyCollection<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft>> GetAllAsync(CancellationToken ct = default)
        => _getAll.ExecuteAsync(ct);

    public Task<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft?> GetByIdAsync(int id, CancellationToken ct = default)
        => _getById.ExecuteAsync(AircraftId.Create(id), ct);

    public Task<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft> CreateAsync(
        int id,
        string registration,
        DateTime dateManufactured,
        bool isActive,
        CancellationToken ct = default)
        => _create.ExecuteAsync(id, registration, dateManufactured, isActive, ct);

    public Task<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft> UpdateAsync(
        int id,
        string registration,
        DateTime dateManufactured,
        bool isActive,
        CancellationToken ct = default)
        => _update.ExecuteAsync(id, registration, dateManufactured, isActive, ct);

    public Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        => _delete.ExecuteAsync(AircraftId.Create(id), ct);
}
