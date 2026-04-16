using System;
using GestorDeVuelosProyectoFinal.src.Moduls.PaymentStatuses.Domain.ValueObject;
namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentStatuses.Domain.Aggregate;

public sealed class PaymentStatuses
{
    public PaymentStatuseId Id { get; private set; } = null!;
    public PaymentStatuseName Name { get; private set; } = null!;

    private PaymentStatuses() { }

    private PaymentStatuses(
        PaymentStatuseId id,
        PaymentStatuseName name)
    {
        Id = id;
        Name = name;
    }

    public static PaymentStatuses Create(
        Guid id,
        string name)
    {
        return new PaymentStatuses(
            PaymentStatuseId.Create(id),
            PaymentStatuseName.Create(name)
        );
    }

    public void UpdateName(string newName)
    {
        // El Value Object PaymentStatuseName se encarga de validar (longitud, números, etc.)
        Name = PaymentStatuseName.Create(newName);
    }
}
