using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Shared.Context;

namespace GestorDeVuelosProyectoFinal.Moduls.Continents.Infrastructure.Persistence.Repositories;

public sealed class ContinentRepository : IContinentsRepository
{
    private readonly AppDbContext _context;

    public ContinentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Continent?> GetByIdAsync(ContinentsId id)
    {
        var entity = await _context.Continents
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id.Value.GetHashCode());

        return entity is null ? null : MapToDomain(entity);
    }

    public async Task<IEnumerable<Continent>> GetAllAsync()
    {
        var entities = await _context.Continents
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync();

        return entities.Select(MapToDomain);
    }

    public async Task<Continent?> GetByNameAsync(string name)
    {
        var entity = await _context.Continents
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == name.Trim());

        return entity is null ? null : MapToDomain(entity);
    }

    public async Task SaveAsync(Continent continent)
    {
        var entity = MapToEntity(continent);
        await _context.Continents.AddAsync(entity);

    }

    public async Task UpdateAsync(Continent continent)
    {
        var entity = await _context.Continents
            .FirstOrDefaultAsync(c => c.Name == continent.Name.Value);

        if (entity is null)
            throw new InvalidOperationException($"Continent '{continent.Name.Value}' not found.");

        entity.Name = continent.Name.Value;
        _context.Continents.Update(entity);

    }

    public async Task DeleteAsync(ContinentsId id)
    {
        var entity = await _context.Continents
            .FirstOrDefaultAsync(c => c.Id == id.Value.GetHashCode());

        if (entity is null)
            throw new InvalidOperationException($"Continent with id '{id.Value}' not found.");

        _context.Continents.Remove(entity);
        
    }

    public async Task DeleteByNameAsync(string name)
    {
        var entity = await _context.Continents
            .FirstOrDefaultAsync(c => c.Name == name.Trim());

        if (entity is null)
            throw new InvalidOperationException($"Continent '{name}' not found.");

        _context.Continents.Remove(entity);
        
    }

    // Mappers
  

    private static Continent MapToDomain(ContinentEntity entity)
    {
        return Continent.Create(Guid.NewGuid(), entity.Name);
    }

    private static ContinentEntity MapToEntity(Continent continent)
    {
        return new ContinentEntity
        {
            Name = continent.Name.Value
        };
    }
}