using GestorDeVuelosProyectoFinal.src.Moduls.Checkins.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Infrastructure.Entity;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Infrastructure.Entity;

public sealed class BoardingPassEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public int CheckinId { get; set; }
    public int TicketId { get; set; }
    public int FlightId { get; set; }
    public string Gate { get; set; } = null!;
    public string SeatCode { get; set; } = null!;
    public DateTime BoardingAt { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public CheckinEntity Checkin { get; set; } = null!;
    public TicketEntity Ticket { get; set; } = null!;
    public FlightEntity Flight { get; set; } = null!;
}
