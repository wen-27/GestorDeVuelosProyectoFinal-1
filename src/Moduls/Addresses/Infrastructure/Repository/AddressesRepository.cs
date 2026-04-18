using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Infrastructure.Persistence.Entities;

namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Infrastructure.Repository;

public sealed class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Address?> GetByIdAsync(AddressesId id, CancellationToken ct = default)
    {
        // Importante: Asegúrate de que en AppDbContext la propiedad se llame 'Addresses'
        var entity = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id.Value, ct);

        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<Address?> GetByStreetAndNumberAsync(string streetName, string? number, CancellationToken ct = default)
    {
        var streetLower = streetName.Trim().ToLower();
        
        var entity = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => 
                x.StreetName.ToLower() == streetLower && 
                x.Number == number, ct);

        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<IReadOnlyCollection<Address>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _context.Addresses.AsNoTracking().ToListAsync(ct);
        return entities.Select(MapToDomain).ToList();
    }

    public async Task SaveAsync(Address address, CancellationToken ct = default)
    {
        var entity = MapToEntity(address);
        await _context.Addresses.AddAsync(entity, ct);
    }

    public async Task UpdateAsync(Address address, CancellationToken ct = default)
    {
        var entity = MapToEntity(address);
        _context.Addresses.Update(entity);
        await Task.CompletedTask;
    }

    public async Task<bool> DeleteAsync(AddressesId id, CancellationToken ct = default)
    {
        // FindAsync no acepta CancellationToken de la misma forma que FirstOrDefault
        var entity = await _context.Addresses.FindAsync(new object[] { id.Value }, ct);
        if (entity == null) return false;

        _context.Addresses.Remove(entity);
        return true;
    }

    public async Task<bool> DeleteByStreetAndNumberAsync(string streetName, string? number, CancellationToken ct = default)
    {
        var streetLower = streetName.Trim().ToLower();
        
        var entity = await _context.Addresses
            .FirstOrDefaultAsync(x => 
                x.StreetName.ToLower() == streetLower && 
                x.Number == number, ct);

        if (entity == null) return false;

        _context.Addresses.Remove(entity);
        return true;
    }

    public async Task<IReadOnlyCollection<Address>> GetByCityAsync(CityId cityId, CancellationToken ct = default)
    {
        var entities = await _context.Addresses
            .AsNoTracking()
            .Where(x => x.CityId == cityId.Value)
            .ToListAsync(ct);
        return entities.Select(MapToDomain).ToList();
    }

    public async Task<Address?> GetByNameAsync(AddressesNameVia name, CancellationToken ct = default)
    {
        var nameLower = name.Value.ToLower();
        var entity = await _context.Addresses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.StreetName.ToLower() == nameLower, ct);
        return entity == null ? null : MapToDomain(entity);
    }

    public async Task<IReadOnlyCollection<Address>> GetByPostalCodeAsync(AddressesPostalCode postalCode, CancellationToken ct = default)
    {
        var entities = await _context.Addresses
            .AsNoTracking()
            .Where(x => x.PostalCode == postalCode.Value)
            .ToListAsync(ct);
        return entities.Select(MapToDomain).ToList();
    }

    public async Task<IReadOnlyCollection<Address>> GetByStreetTypeAsync(int streetTypeId, CancellationToken ct = default)
    {
        var entities = await _context.Addresses
            .AsNoTracking()
            .Where(x => x.StreetTypeId == streetTypeId)
            .ToListAsync(ct);
            
        return entities.Select(MapToDomain).ToList();
    }

    // --- MAPPERS INTERNOS ---

    private static Address MapToDomain(AddressEntity entity)
    {
        return Address.Create(
            entity.Id,
            entity.StreetTypeId,
            entity.StreetName,
            entity.Number,
            entity.Complement,
            entity.PostalCode,
            entity.CityId
        );
    }

    private static AddressEntity MapToEntity(Address domain)
    {
        return new AddressEntity
        {
            Id = domain.Id.Value,
            StreetTypeId = domain.RoadTypeId.Value,
            StreetName = domain.StreetName.Value,
            Number = domain.Number.Value,
            Complement = domain.Complement.Value,
            CityId = domain.CityId.Value,
            PostalCode = domain.PostalCode.Value
        };
    }
}