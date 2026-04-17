using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Infrastructure.Persistence.Entities;

namespace GestorDeVuelosProyectoFinal.Moduls.Cities.Infrastructure.Repository;

public sealed class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context) => _context = context;

    public async Task<City?> GetByIdAsync(CityId id)
    {
        var entity = await _context.Cities.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id.Value);
        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<City?> GetByNameAsync(string name)
    {
        var entity = await _context.Cities.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name.ToLower() == name.Trim().ToLower());
        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<IEnumerable<City>> GetAllAsync()
    {
        var entities = await _context.Cities.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
        return entities.Select(MapToDomain);
    }

    public async Task<IEnumerable<City>> GetByCountryAsync(RegionId regionId)
    {
        var entities = await _context.Cities.AsNoTracking()
            .Where(c => c.RegionId == regionId.Value).ToListAsync();
        return entities.Select(MapToDomain);
    }

    public async Task SaveAsync(City city) => await _context.Cities.AddAsync(MapToEntity(city));
    public async Task saveAsync(City city) => await SaveAsync(city);

    public async Task DeleteAsync(CityId id)
    {
        var entity = await _context.Cities.FirstOrDefaultAsync(c => c.Id == id.Value);
        if (entity != null) _context.Cities.Remove(entity);
    }

    public async Task DeleteByNameAsync(string name)
    {
        var entity = await _context.Cities.FirstOrDefaultAsync(c => c.Name == name);
        if (entity != null) _context.Cities.Remove(entity);
    }

    public async Task DeleteByCountryAsync(RegionId regionId)
    {
        var entities = await _context.Cities.Where(c => c.RegionId == regionId.Value).ToListAsync();
        _context.Cities.RemoveRange(entities);
    }

    // Mappers
    private static City MapToDomain(CityEntity entity) => City.Create(entity.Id, entity.Name, entity.RegionId);
    private static CityEntity MapToEntity(City city) => new() { Id = city.Id.Value, Name = city.Name.Value, RegionId = city.RegionId.Value };
}