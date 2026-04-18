namespace GestorDeVuelosProyectoFinal.Moduls.Aircraft.Application.Interfaces;

public interface IAircraftService
{
    Task<Domain.Aggregate.Aircraft> CreateAsync(
        int id,
        string aircraftRegistration, 
        DateTime dateManufactured, 
        bool isActive, 
        CancellationToken cancellationToken = default);

    Task<Domain.Aggregate.Aircraft?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Domain.Aggregate.Aircraft>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Domain.Aggregate.Aircraft> UpdateAsync(
        int id, 
        string aircraftRegistration, 
        DateTime dateManufactured, 
        bool isActive, 
        CancellationToken cancellationToken = default);
    
    Task <bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
