using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PaymentMediumTypes.Domain.ValueObject;

public sealed class PaymentMediumTypesId
{
    public Guid Value { get; }

    private PaymentMediumTypesId(Guid value) => Value = value;

    public static PaymentMediumTypesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del tipo de medio de pago no es válido", nameof(value));

        return new PaymentMediumTypesId(value);
    }
}
