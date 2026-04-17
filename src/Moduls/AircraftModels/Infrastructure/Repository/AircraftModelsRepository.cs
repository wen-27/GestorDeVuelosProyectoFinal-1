using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Context;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Infrastructure.Repository;

public class AircraftModelsRepository : IAircraftModelsRepository
{
    private readonly AppDbContext _dbcontext;
    public AircraftModelsRepository(AppDbContext dbcontext) => _dbcontext = dbcontext;

    public async Task AddAsync(AircraftModel model, CancellationToken cancellationToken = default)
    {
        var entity = new AircraftModelsEntity
        {
            Id = model.Id.Value,
            Name = model.ModelName.Value,
            Capacity = model.MaxCapacity.Value,
            Weight = model.MaxTakeoffWeight.Value,
            FuelConsumption = model.FuelConsumption.Value,
            CruiseSpeed = model.CruiseSpeed.Value,
            CruiseAltitude = model.CruiseAltitude.Value
        };

        await _dbcontext.AircraftModels.AddAsync(entity, cancellationToken); // añadir dentro de AppDbContext AircraftModels
        await _dbcontext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> DeleteByIdAsync(AircraftModelId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<AircraftModel>> FindAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<AircraftModel?> FindByIdAsync(AircraftModelId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(AircraftModel model, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}