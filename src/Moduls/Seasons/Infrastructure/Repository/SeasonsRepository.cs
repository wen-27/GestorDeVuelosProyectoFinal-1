using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Infrastructure.Entities;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.Infrastructure.Repository;

public sealed class SeasonsRepository : ISeasonsRepository
{
    private readonly AppDbContext _context;

    public SeasonsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Season?> GetByIdAsync(SeasonsId id)
    {
        var entity = await _context.Seasons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        return entity is null ? null : MapToDomain(entity);
    }

    public async Task<Season?> GetByNameAsync(SeasonsName name)
    {
        var normalized = name.Value.Trim().ToLower();

        var entity = await _context.Seasons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.ToLower() == normalized);

        return entity is null ? null : MapToDomain(entity);
    }

    public async Task<IEnumerable<Season>> GetAllAsync()
    {
        var entities = await _context.Seasons
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();

        return entities.Select(MapToDomain);
    }

    public async Task SaveAsync(Season season)
    {
        await _context.Seasons.AddAsync(MapToEntity(season));
    }

    public Task UpdateAsync(Season season)
    {
        _context.Seasons.Update(MapToEntity(season));
        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(SeasonsId id)
    {
        var entity = await _context.Seasons.FirstOrDefaultAsync(x => x.Id == id.Value);
        if (entity is not null)
            _context.Seasons.Remove(entity);
    }

    public async Task DeleteByNameAsync(SeasonsName name)
    {
        var normalized = name.Value.Trim().ToLower();
        var entity = await _context.Seasons.FirstOrDefaultAsync(x => x.Name.ToLower() == normalized);
        if (entity is not null)
            _context.Seasons.Remove(entity);
    }

    private static Season MapToDomain(SeasonEntity entity)
    {
        return Season.FromPrimitives(entity.Id, entity.Name, entity.Description, entity.PriceFactor);
    }

    private static SeasonEntity MapToEntity(Season aggregate)
    {
        return new SeasonEntity
        {
            Id = aggregate.Id?.Value ?? 0,
            Name = aggregate.Name.Value,
            Description = aggregate.Description.Value,
            PriceFactor = aggregate.PriceFactor.Value
        };
    }
}
