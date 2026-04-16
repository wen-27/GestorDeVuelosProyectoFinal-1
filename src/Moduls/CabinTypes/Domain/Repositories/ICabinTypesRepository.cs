using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Repositories;

public interface ICabinTypesRepository
{
    Task<CabinType?> GetByIdAsync(CabinTypesId id);
    
    // Para buscar "Economy", "Business", "First Class", etc.
    Task<CabinType?> GetByNameAsync(CabinTypesName name);

    Task<IEnumerable<CabinType>> GetAllAsync();
    
    Task SaveAsync(CabinType cabinType);
    Task DeleteAsync(CabinTypesId id);
}