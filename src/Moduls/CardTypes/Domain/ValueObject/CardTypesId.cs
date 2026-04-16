using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CardTypes.Domain.ValueObject;

public sealed class CardTypesId
{
    public Guid Value { get; }

    private CardTypesId(Guid value) => Value = value;

    public static CardTypesId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del tipo de tarjeta no es válido", nameof(value));

        return new CardTypesId(value);
    }
}
