using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.Moduls.FlightAssignments.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.Moduls.FlightAssignments.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.FlightAssignments.Domain.Repositories;

public interface IFlightAssignmentsRepository
{
    Task<FlightAssignment?> GetByIdAsync(FlightAssignmentId id);
    
    // Para validar que no se repita el personal en el mismo vuelo (UNIQUE)
    Task<FlightAssignment?> GetByFlightAndStaffAsync(FlightsId flightId, StaffAvailabilityId staffId);

    Task<IEnumerable<FlightAssignment>> GetByFlightIdAsync(FlightsId flightId);
    
    Task SaveAsync(FlightAssignment assignment);
    Task DeleteAsync(FlightAssignmentId id);
}
