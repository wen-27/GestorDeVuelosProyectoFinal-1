using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.Repositories;

public interface IAirlinesRepository
{
    // Búsqueda por el Identificador único (GUID)
    Task<Airline?> GetByIdAsync(AirlinesId id);

    // Búsqueda por Código IATA (ej: "AV", "LAT") - Muy útil por ser UNIQUE
    Task<Airline?> GetByIataCodeAsync(AirlinesIataCode code);

    // Obtener todas las aerolíneas de un país específico
    Task<IEnumerable<Airline>> GetByCountryIdAsync(CountryId countryId);

    // Obtener todas las aerolíneas
    Task<IEnumerable<Airline>> GetAllAsync();

    // Obtener solo las aerolíneas que están marcadas como activas
    Task<IEnumerable<Airline>> GetActiveAsync();

    // Guardar o Actualizar una aerolínea
    Task SaveAsync(Airline airline);

    // Eliminar lógicamente o físicamente una aerolínea
    Task DeleteAsync(AirlinesId id);
}