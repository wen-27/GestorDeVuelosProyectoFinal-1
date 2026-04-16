using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.InvoiceItems.Domain.ValueObject;

public sealed class InvoiceItemsId
{
    public Guid Value { get; }
    private InvoiceItemsId(Guid value) => Value = value;

    public static InvoiceItemsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del ítem de factura no es válido.");
        return new InvoiceItemsId(value);
    }
}