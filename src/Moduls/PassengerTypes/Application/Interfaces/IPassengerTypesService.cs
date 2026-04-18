using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Aggregate;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Application.Interfaces;

public interface IPassengerTypesService
{
    Task<IReadOnlyCollection<PassengerType>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PassengerType?> GetByIdAsync(int id, CancellationToken cancellationToken = default);

    Task<PassengerType?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<PassengerType?> GetByAgeAsync(int ageInYears, CancellationToken cancellationToken = default);

    /// <summary>Resuelve el tipo segun fecha de nacimiento y fecha de referencia (por defecto hoy UTC).</summary>
    Task<PassengerType?> ResolveByBirthDateAsync(DateTime birthDate, DateTime? referenceDateUtc = null, CancellationToken cancellationToken = default);

    Task CreateAsync(string name, int? minAge, int? maxAge, CancellationToken cancellationToken = default);

    Task UpdateAsync(int id, string name, int? minAge, int? maxAge, CancellationToken cancellationToken = default);

    Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default);

    Task DeleteByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<int> DeleteByAgeAsync(int ageInYears, CancellationToken cancellationToken = default);
}
