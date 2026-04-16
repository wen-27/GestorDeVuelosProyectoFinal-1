using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Payments.Domain.ValueObject;   

namespace GestorDeVuelosProyectoFinal.src.Moduls.Payments.Domain.Aggregate;

public sealed class Payments
{
    public PaymentsId Id { get; private set; } = null!;
    public PaymentsDate Date { get; private set; } = null!;
    public PaymentsAmount Amount { get; private set; } = null!;
    public PaymentsCreatedAt CreatedAt { get; private set; } = null!;
    public PaymentsUpdatedAt UpdatedAt { get; private set; } = null!;

    private Payments() { }

    private Payments(
        PaymentsId id,
        PaymentsDate date,
        PaymentsAmount amount,
        PaymentsCreatedAt createdAt,
        PaymentsUpdatedAt updatedAt)
    {
        Id = id;
        Date = date;
        Amount = amount;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Payments Create(
        Guid id,
        DateTime date,
        decimal amount,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new Payments(
            PaymentsId.Create(id),
            PaymentsDate.Create(date),
            PaymentsAmount.Create(amount),
            PaymentsCreatedAt.Create(createdAt),
            PaymentsUpdatedAt.Create(updatedAt)
        );
    }

    public void UpdateDate(DateTime newDate)
    {
        // El Value Object PaymentsDate se encarga de validar (fecha no vacia, fecha en el pasado, etc.)
        Date = PaymentsDate.Create(newDate);
    }
}
