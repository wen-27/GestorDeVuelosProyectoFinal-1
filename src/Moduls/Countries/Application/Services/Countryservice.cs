using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Application.UseCases;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Countries.Application.Services;

public sealed class CountryService : ICountryService
{
    private readonly GetCountriesUseCase _getAll;
    private readonly CreateCountryUseCase _create;
    private readonly UpdateCountryUseCase _update;
    private readonly DeleteCountryUseCase _delete;

    public CountryService(
        GetCountriesUseCase getAll,
        CreateCountryUseCase create,
        UpdateCountryUseCase update,
        DeleteCountryUseCase delete)
    {
        _getAll = getAll;
        _create = create;
        _update = update;
        _delete = delete;
    }

    public Task<IEnumerable<Country>> GetAllAsync()
        => _getAll.ExecuteAsync();

    public Task<IEnumerable<Country>> GetByContinentIdAsync(int continentId)
        => _getAll.GetByContinentAsync(ContinentsId.Create(continentId));

    public Task<Country?> GetByIsoCodeAsync(string isoCode)
        => _getAll.GetByIsoCodeAsync(CountryIsoCode.Create(isoCode));

    public Task<Country?> GetByNameAsync(string name)
        => _getAll.GetByNameAsync(name);

    public Task CreateAsync(string name, string isoCode, int continentId)
        => _create.ExecuteAsync(name, isoCode, continentId);

    public Task UpdateAsync(string currentIsoCode, string newName, string newIsoCode, int newContinentId)
        => _update.ExecuteAsync(currentIsoCode, newName, newIsoCode, newContinentId);

    public Task DeleteByNameAsync(string name)
        => _delete.DeleteByNameAsync(name);

    public Task DeleteByIsoCodeAsync(string isoCode)
        => _delete.DeleteByIsoCodeAsync(isoCode);
}