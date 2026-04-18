using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Repositories;
public interface ICabinTypesRepository
{
    // Consultas usando Value Objects o Primitivos (Está bien tener ambos)
    Task<CabinType?> GetByIdAsync(CabinTypesId id);
    Task<CabinType?> GetByIdIntAsync(int id);
    Task<CabinType?> GetByNameAsync(CabinTypesName name);
    Task<CabinType?> GetByNameStringAsync(string name);
    Task<IEnumerable<CabinType>> GetAllAsync();

    // Persistencia (SIEMPRE usan la Entidad de Dominio)
    Task SaveAsync(CabinType cabinType);
    Task UpdateAsync(CabinType cabinType); // ELIMINA el que recibe (int id, string name)
    
    // Eliminación
    Task DeleteAsync(CabinTypesId id);
    Task DeleteByNameAsync(string name);
}