using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Application.UseCases;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Aggregate;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Application.Services;

public sealed class PassengerTypesService : IPassengerTypesService
{
    private readonly QueryPassengerTypesUseCase _query;
    private readonly ResolvePassengerTypeByAgeUseCase _resolveByAge;
    private readonly CreatePassengerTypeUseCase _create;
    private readonly UpdatePassengerTypeUseCase _update;
    private readonly DeletePassengerTypesUseCase _delete;

    public PassengerTypesService(
        QueryPassengerTypesUseCase query,
        ResolvePassengerTypeByAgeUseCase resolveByAge,
        CreatePassengerTypeUseCase create,
        UpdatePassengerTypeUseCase update,
        DeletePassengerTypesUseCase delete)
    {
        _query = query;
        _resolveByAge = resolveByAge;
        _create = create;
        _update = update;
        _delete = delete;
    }

    public Task<IReadOnlyCollection<PassengerType>> GetAllAsync(CancellationToken cancellationToken = default)
        => _query.GetAllAsync(cancellationToken);

    public Task<PassengerType?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        => _query.GetByIdAsync(id, cancellationToken);

    public Task<PassengerType?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
        => _query.GetByNameAsync(name, cancellationToken);

    public Task<PassengerType?> GetByAgeAsync(int ageInYears, CancellationToken cancellationToken = default)
        => _query.GetByAgeAsync(ageInYears, cancellationToken);

    public Task<PassengerType?> ResolveByBirthDateAsync(DateTime birthDate, DateTime? referenceDateUtc = null, CancellationToken cancellationToken = default)
        => _resolveByAge.ExecuteAsync(birthDate, referenceDateUtc, cancellationToken);

    public Task CreateAsync(string name, int? minAge, int? maxAge, CancellationToken cancellationToken = default)
        => _create.ExecuteAsync(name, minAge, maxAge, cancellationToken);

    public Task UpdateAsync(int id, string name, int? minAge, int? maxAge, CancellationToken cancellationToken = default)
        => _update.ExecuteAsync(id, name, minAge, maxAge, cancellationToken);

    public Task DeleteByIdAsync(int id, CancellationToken cancellationToken = default)
        => _delete.DeleteByIdAsync(id, cancellationToken);

    public Task DeleteByNameAsync(string name, CancellationToken cancellationToken = default)
        => _delete.DeleteByNameAsync(name, cancellationToken);

    public Task<int> DeleteByAgeAsync(int ageInYears, CancellationToken cancellationToken = default)
        => _delete.DeleteByAgeAsync(ageInYears, cancellationToken);
}
