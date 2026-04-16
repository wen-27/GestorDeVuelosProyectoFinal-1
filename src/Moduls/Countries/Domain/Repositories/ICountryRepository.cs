using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.Repositories;

public interface ICountriesRepository
{
    // Buscar un país por su ID único
    Task<Country?> GetByIdAsync(CountryId id);

    // Buscar un país por su código ISO (ej: "COL", "ESP")
    Task<Country?> GetByIsoCodeAsync(CountryIsoCode isoCode);

    // Obtener todos los países de un continente específico
    Task<IEnumerable<Country>> GetByContinentAsync(ContinentsId continentId);

    // Obtener la lista completa de países
    Task<IEnumerable<Country>> GetAllAsync();

    // Guardar o actualizar un país
    Task SaveAsync(Country country);

    // Eliminar un país del sistema
    Task DeleteAsync(CountryId id);
}