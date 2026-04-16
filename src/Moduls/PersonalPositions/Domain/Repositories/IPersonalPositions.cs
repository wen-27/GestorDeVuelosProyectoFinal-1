using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.Repositories;

public interface IPersonalPositionsRepository
{
    Task<PersonalPosition?> GetByIdAsync(PersonalPositionsId id);
    
    // Aprovechando el UNIQUE de tu base de datos para buscar por nombre (Piloto, etc.)
    Task<PersonalPosition?> GetByNameAsync(PersonalPositionsName name);

    Task<IEnumerable<PersonalPosition>> GetAllAsync();
    
    Task SaveAsync(PersonalPosition position);
    Task DeleteAsync(PersonalPositionsId id);
}