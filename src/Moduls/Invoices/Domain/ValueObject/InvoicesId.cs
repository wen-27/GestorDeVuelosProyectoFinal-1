using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Invoices.Domain.ValueObject;

public sealed class InvoicesId
{
    public Guid Value { get; }
    private InvoicesId(Guid value) => Value = value;

    public static InvoicesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la factura no es válido.");
        return new InvoicesId(value);
    }
}