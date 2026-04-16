using System;

namespace GestorDeVuelosProyectoFinal.Moduls.InvoiceItemTypes.Domain.ValueObject;

public sealed class InvoiceItemTypesId
{
    public Guid Value { get; }
    private InvoiceItemTypesId(Guid value) => Value = value;
    public static InvoiceItemTypesId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID del tipo de artículo no es válido.");
        return new InvoiceItemTypesId(value);
    }
}