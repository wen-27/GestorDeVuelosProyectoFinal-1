using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Payments.Domain.ValueObject;

public sealed class PaymentsId
{
    public Guid Value { get; }    
    private PaymentsId(Guid value) => Value = value;
    public static PaymentsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la reserva no es válido", nameof(value));

        return new PaymentsId(value);
    }
}
