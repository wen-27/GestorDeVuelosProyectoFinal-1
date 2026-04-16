using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentStatuses.Domain.ValueObject;

public sealed class PaymentStatuseId
{
    public Guid Value { get; }

    private PaymentStatuseId(Guid value) => Value = value;

    public static PaymentStatuseId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del estado de pago no es válido", nameof(value));

        return new PaymentStatuseId(value);
    }
}
