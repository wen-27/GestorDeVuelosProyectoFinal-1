namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.Interfaces;

public interface IAircraftService
{
    Task<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft> CreateAsync(
        int id,
        string aircraftRegistration, 
        DateTime dateManufactured, 
        bool isActive, 
        CancellationToken cancellationToken = default);

    Task<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<global::GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft> UpdateAsync(
        int id, 
        string aircraftRegistration, 
        DateTime dateManufactured, 
        bool isActive, 
        CancellationToken cancellationToken = default);
    
    Task <bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
