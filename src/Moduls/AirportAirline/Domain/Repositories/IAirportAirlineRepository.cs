using GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.Repositories;

public interface IAirportAirlineRepository
{
    // Usamos el nombre completo de la clase para evitar ambigüedad con el namespace
    Task<Aggregate.AirportAirline?> GetByIdAsync(AirportAirlineId id);
    Task<IEnumerable<Aggregate.AirportAirline>> GetAllAsync();
    Task SaveAsync(Aggregate.AirportAirline airportAirline);        
    Task DeleteAsync(AirportAirlineId id);  
}