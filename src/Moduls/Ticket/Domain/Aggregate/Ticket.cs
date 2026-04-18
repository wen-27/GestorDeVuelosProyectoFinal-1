using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Domain.Aggregate;

public sealed class Ticket
{
    public TicketId Id { get; private set; } = null!;
    public TicketCode Code { get; private set; } = null!;
    public TicketIssueDate IssueDate { get; private set; } = null!;
    public TicketCreatedAt CreatedAt { get; private set; } = null!;
    public TicketUpdateAt UpdateAt { get; private set; } = null!;

    private Ticket() { }

    private Ticket(
        TicketId id,
        TicketCode code,
        TicketIssueDate issueDate,
        TicketCreatedAt createdAt,
        TicketUpdateAt updateAt)
    {
        Id = id;
        Code = code;
        IssueDate = issueDate;
        CreatedAt = createdAt;
        UpdateAt = updateAt;
    }

    public static Ticket Create(
        int id,
        string code,
        DateTime issueDate,
        DateTime updatedAt,
        DateTime createdAt)
    {
        return new Ticket(
            TicketId.Create(id),
            TicketCode.Create(code),
            TicketIssueDate.Create(issueDate),
            TicketCreatedAt.Create(createdAt),
            TicketUpdateAt.Create(updatedAt)
        );
    }

    public void UpdateCode(string newCode)
    {
        // El Value Object TicketCode se encarga de validar (longitud, números, etc.)
        Code = TicketCode.Create(newCode);
    }

    public void UpdateIssueDate(DateTime newIssueDate)
    {
        // El Value Object TicketIssueDate se encarga de validar (fecha no vacia, fecha en el pasado, etc.)
        IssueDate = TicketIssueDate.Create(newIssueDate);
    }

    public void UpdateCreatedAt(DateTime newCreatedAt)
    {
        // El Value Object TicketCreatedAt se encarga de validar (fecha no vacia, fecha en el pasado, etc.)
        CreatedAt = TicketCreatedAt.Create(newCreatedAt);
    }

    public void UpdateUpdateAt(DateTime newUpdateAt)
    {
        // El Value Object TicketUpdateAt se encarga de validar (fecha no vacia, fecha en el pasado, etc.)
        UpdateAt = TicketUpdateAt.Create(newUpdateAt);
    }
}
