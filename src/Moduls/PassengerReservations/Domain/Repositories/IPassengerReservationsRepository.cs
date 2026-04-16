using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Domain.Repositories;

public interface IPassengerReservationsRepository
{
    Task<PassengerReservation?> GetByIdAsync(PassengerReservationId id);
    Task<PassengerReservation?> GetByFlightAndPassengerAsync(FlightReservationId flightReservationId, PassengersId passengerId);
    Task<IEnumerable<PassengerReservation>> GetByFlightReservationIdAsync(FlightReservationId flightReservationId);
    Task SaveAsync(PassengerReservation passengerReservation);
    Task DeleteAsync(PassengerReservationId id);
}