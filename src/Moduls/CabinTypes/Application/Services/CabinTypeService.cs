using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Application.UseCases;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Aggregate;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinTypes.Application.Services;

public  class CabinTypeService : ICabinTypeService
{
    public Task CreateAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task DeleteByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CabinType>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(int id, string name)
    {
        throw new NotImplementedException();
    }

    public interface ICabinTypeService
{
    Task CreateAsync(string name);
    Task<IEnumerable<CabinTypeResponse>> GetAllAsync(); 
    Task DeleteByIdAsync(int id);
    Task UpdateAsync(int id, string name); 
    Task DeleteByNameAsync(string name);
}
}