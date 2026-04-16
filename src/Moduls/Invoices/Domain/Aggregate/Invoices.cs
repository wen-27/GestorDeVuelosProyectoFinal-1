using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Invoices.Domain.ValueObject;

using GestorDeVuelosProyectoFinal.src.Moduls.Reservations.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Invoices.Domain.Aggregate;

public sealed class Invoice
{
    public InvoicesId Id { get; private set; } = null!;

    public ReverseId ReservaId { get; private set; } = null!; 
    public InvoicesNumber NumeroFactura { get; private set; } = null!;
    public InvoicesDate FechaEmision { get; private set; } = null!;
    public InvoicesAmount Subtotal { get; private set; } = null!;
    public InvoicesAmount Impuestos { get; private set; } = null!;
    public InvoicesAmount Total { get; private set; } = null!;

    private Invoice() { }

    public static Invoice Create(
        Guid id,
        Guid reservaId,
        string numeroFactura,
        DateTime fechaEmision,
        decimal subtotal,
        decimal impuestos,
        decimal total)
    {
        return new Invoice
        {
            Id = InvoicesId.Create(id),
            ReservaId = ReverseId.Create(reservaId), // Ajustado aquí también
            NumeroFactura = InvoicesNumber.Create(numeroFactura),
            FechaEmision = InvoicesDate.Create(fechaEmision),
            Subtotal = InvoicesAmount.Create(subtotal, "subtotal"),
            Impuestos = InvoicesAmount.Create(impuestos, "impuestos"),
            Total = InvoicesAmount.Create(total, "total")
        };
    }
}