using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Aggregate;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Application.Interfaces;

public interface ICabinTypeService
{
    Task CreateAsync(string name);
    Task<IEnumerable<Domain.Aggregate.CabinType>> GetAllAsync(); 
    Task DeleteByIdAsync(int id);
    Task DeleteByNameAsync(string name);
    Task UpdateAsync(int id, string name); 
}
