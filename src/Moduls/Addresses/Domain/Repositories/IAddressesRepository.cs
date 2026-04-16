using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Repositories;

public interface IAddressRepository
{
    // Crear
    Task SaveAsync(Address address, CancellationToken cancellationToken = default);
     // Leer
    Task<Address?> GetByIdAsync(AddressesId id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Address>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Address?> GetByNameAsync(AddressesNameVia name, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Address>> GetByCityAsync(CityId cityId, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Address>> GetByPostalCodeAsync(AddressesPostalCode postalCode, CancellationToken cancellationToken = default);
    Task UpdateAsync(Address address, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(AddressesId id, CancellationToken cancellationToken = default);
}