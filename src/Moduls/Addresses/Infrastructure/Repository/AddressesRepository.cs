using Microsoft.EntityFrameworkCore;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Aggregate; 
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Repositories;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;
//using GestorDeVuelosProyectoFinal.src.Moduls.Addresses.Infrastructure.Entity;


namespace GestorDeVuelosProyectoFinal.src.Moduls.Addresses.Infrastructure.Repository;

public sealed class AddressesRepository : IAddressRepository
{
    private readonly AppDbContext _dbcontext;

    public AddressesRepository(AppDbContext dbContext)
    {
        _dbcontext = dbContext;
    }

    public Task<bool> DeleteAsync(AddressesId id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Address>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Address>> GetByCityAsync(CityId cityId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<Address?> GetByIdAsync(AddressesId id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id.Value.ToString()))
            throw new ArgumentException("El ID de la dirección no puede estar vacío.", nameof(id));
        var address = await _dbcontext.Addresses
            .FirstOrDefaultAsync(a => a.Id == id.Value, cancellationToken);
        if (address == null)
            return null;
        return new Address(new AddressesId(address.Id),new RoadTypeId(address.ViaType_id),new AddressesNameVia(address.PathName),new AddressesNumber(address.Number),new AddressesComplement(address.Complement),new AddressesPostalCode(address.Postal_code),new CityId(address.Cities_id));
    }

    public Task<Address?> GetByNameAsync(AddressesNameVia name, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Address>> GetByPostalCodeAsync(AddressesPostalCode postalCode, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(Address address, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Address address, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }



    // ... Implementar los demás métodos siguiendo la misma lógica
}