using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.Repositories;

public interface ISeasonsRepository
{
    Task<Season?> GetByIdAsync(SeasonsId id);
    
    // Buscar por nombre (útil para validar el UNIQUE de la base de datos)
    Task<Season?> GetByNameAsync(SeasonsName name);

    Task<IEnumerable<Season>> GetAllAsync();
    
    Task SaveAsync(Season season);
    Task DeleteAsync(SeasonsId id);
}