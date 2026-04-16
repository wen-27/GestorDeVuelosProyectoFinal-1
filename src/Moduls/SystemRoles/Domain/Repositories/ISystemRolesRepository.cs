using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.src.Moduls.Roles.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.Roles.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Roles.Domain.Repositories;

public interface ISystemRolesRepository
{
    Task<SystemRoles?> GetByIdAsync(RolesId id);
    
    // Método para validar el UNIQUE de la columna 'nombre'
    Task<SystemRoles?> GetByNameAsync(RolesName name); 
    
    Task<IEnumerable<SystemRoles>> GetAllAsync();
    Task SaveAsync(SystemRoles role);
    Task DeleteAsync(RolesId id);
}