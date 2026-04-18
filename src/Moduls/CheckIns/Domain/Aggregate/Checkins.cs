using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Checkins.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.ValueObject;
// Importación corregida según tu archivo TicketId
using GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Checkins.Domain.Aggregate;

public sealed class Checkin
{
    public CheckinsId Id { get; private set; } = null!;
    public CheckinsBoardingPassNumber BoardingPassNumber { get; private set; } = null!;
    public CheckinsDate Date { get; private set; } = null!;
    public CheckinsHasHoldBaggage HasHoldBaggage { get; private set; } = null!;
    public CheckinsBaggageWeight BaggageWeight { get; private set; } = null!;

    // FKs sincronizadas
    public TicketId TicketId { get; private set; } = null!; // Antes era TiquetesId
    public StaffAvailabilityId PersonalId { get; private set; } = null!;
    public FlightsId FlightSeatId { get; private set; } = null!;

    private Checkin() { }

    public static Checkin Create(
        int id, 
        string boardingPass, 
        DateTime date, 
        bool hasBaggage, 
        decimal weight,
        int ticketId,
        int personalId,
        int seatId)
    {
        return new Checkin
        {
            Id = CheckinsId.Create(id),
            BoardingPassNumber = CheckinsBoardingPassNumber.Create(boardingPass),
            Date = CheckinsDate.Create(date),
            HasHoldBaggage = CheckinsHasHoldBaggage.Create(hasBaggage),
            BaggageWeight = CheckinsBaggageWeight.Create(weight),
            TicketId = TicketId.Create(ticketId),
            PersonalId = StaffAvailabilityId.Create(personalId),
            FlightSeatId = FlightsId.Create(seatId)
        };
    }
}
