using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentMethods.Domain.ValueObject;

public sealed class PaymentMethodsId
{
    public Guid Value { get; }

    private PaymentMethodsId(Guid value) => Value = value;

    public static PaymentMethodsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del medio de pago no es válido", nameof(value));

        return new PaymentMethodsId(value);
    }
}
