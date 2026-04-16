using System;

namespace GestorDeVuelosProyectoFinal.Moduls.CardIssuers.Domain.ValueObject;

public sealed class CardIssuersId
{
    public Guid Value { get; }
    private CardIssuersId(Guid value) => Value = value;
    public static CardIssuersId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del emisor de tarjetas no es válido.");
        return new CardIssuersId(value);
    }
}