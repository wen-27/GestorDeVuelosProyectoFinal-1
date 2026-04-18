using System;
using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Shared.Context;

using aircraftAggregate = GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Infrastructure.Repository;

public sealed class AircraftRepository : IAircraftRepository
{
    private readonly AppDbContext _context;

    public AircraftRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<aircraftAggregate?> GetByIdAsync(AircraftId id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Aircrafts
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        return entity is null ? null : MapToDomain(entity);
    }

    public async Task<IReadOnlyCollection<aircraftAggregate>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _context.Aircrafts
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);

        return entities.Select(MapToDomain).ToList();
    }

    public async Task AddAsync(aircraftAggregate aircraft, CancellationToken cancellationToken = default)
    {
        var entity = MapToEntity(aircraft);
        await _context.Aircrafts.AddAsync(entity, cancellationToken);
    }

    public async Task UpdateAsync(aircraftAggregate aircraft, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Aircrafts
            .FirstOrDefaultAsync(x => x.Id == aircraft.Id.Value, cancellationToken);

        if (entity is null)
            throw new InvalidOperationException($"Aircraft with id '{aircraft.Id.Value}' not found.");

        entity.Registration = aircraft.Registration.Value;
        entity.DateManufactured = aircraft.DateManufactured.Value;
        entity.IsActive = aircraft.IsActive;
    }

    public async Task<bool> DeleteByIdAsync(AircraftId id, CancellationToken cancellationToken = default)
    {
        var entity = await _context.Aircrafts
            .FirstOrDefaultAsync(x => x.Id == id.Value, cancellationToken);

        if (entity is null)
            return false;

        _context.Aircrafts.Remove(entity);
        return true;
    }

    private static aircraftAggregate MapToDomain(AircraftEntity entity)
    {
        return aircraftAggregate.Create(
            entity.Id,
            entity.Registration,
            entity.DateManufactured,
            entity.IsActive);
    }

    private static AircraftEntity MapToEntity(aircraftAggregate aircraft)
    {
        return new AircraftEntity
        {
            Id = aircraft.Id.Value,
            Registration = aircraft.Registration.Value,
            DateManufactured = aircraft.DateManufactured.Value,
            IsActive = aircraft.IsActive,

            // Estos campos siguen existiendo en la tabla/entity,
            // pero hoy el agregado Aircraft no los modela todavía.
            // Se dejan con 0 temporalmente hasta que el dominio incluya
            // AircraftModelId y AirlinesId como parte del aggregate.
            AircraftModelId = 0,
            AirlinesId = 0
        };
    }
}
