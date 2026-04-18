using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Infrastructure.Entity;

public class TicketEntity
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public DateTime IssueDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int PassengerReservation_Id { get; set; }
    public int TicketState_Id { get; set; }
}
