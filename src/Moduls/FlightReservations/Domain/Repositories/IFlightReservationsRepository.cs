using System.Collections.Generic;
using System.Threading.Tasks;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;
using FlightsValueObject = GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.FlightReservations.Domain.Repositories;

public interface IFlightReservationsRepository
{
    // Cambiamos todos los tipos a FlightReservation (singular)
    Task<FlightReservation?> GetByIdAsync(FlightReservationId id);

    Task<FlightReservation?> GetByReservationAndFlightAsync(ReverseId reservationId, FlightsValueObject.FlightsId flightId);

    Task<IEnumerable<FlightReservation>> GetByReservationIdAsync(ReverseId reservationId);

    Task SaveAsync(FlightReservation flightReservation);
    Task DeleteAsync(FlightReservationId id);
}