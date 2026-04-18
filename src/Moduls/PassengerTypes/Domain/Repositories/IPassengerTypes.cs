using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Repositories;

public interface IPassengerTypesRepository
{
    Task<PassengerType?> GetByIdAsync(PassengerTypesId id, CancellationToken cancellationToken = default);

    Task<PassengerType?> GetByNameAsync(PassengerTypesName name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Primer tipo cuyo rango [min_age, max_age] contiene la edad (extremos null = sin límite en ese lado).
    /// Si hay solapamientos, gana el de menor id.
    /// </summary>
    Task<PassengerType?> GetByAgeAsync(int ageInYears, CancellationToken cancellationToken = default);

    Task<IReadOnlyCollection<PassengerType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task SaveAsync(PassengerType passengerType, CancellationToken cancellationToken = default);

    Task UpdateAsync(PassengerType passengerType, CancellationToken cancellationToken = default);

    Task DeleteAsync(PassengerTypesId id, CancellationToken cancellationToken = default);

    Task<bool> DeleteByNameAsync(PassengerTypesName name, CancellationToken cancellationToken = default);

    /// <summary>Elimina todos los tipos cuyo rango contiene la edad indicada.</summary>
    Task<int> DeleteByAgeAsync(int ageInYears, CancellationToken cancellationToken = default);
}
