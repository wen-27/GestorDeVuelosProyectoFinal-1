using System;
using GestorDeVuelosProyectoFinal.Moduls.CardIssuers.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.CardIssuers.Domain.Aggregate;

public sealed class CardIssuer
{
    public CardIssuersId Id { get; private set; } = null!;
    public CardIssuersName Name { get; private set; } = null!;
    public CardIssuersIssuerNumber IssuerNumber { get; private set; } = null!;

    private CardIssuer() { }

    private CardIssuer(CardIssuersId id, CardIssuersName name, CardIssuersIssuerNumber issuerNumber)
    {
        Id = id;
        Name = name;
        IssuerNumber = issuerNumber;
    }

    public static CardIssuer Create(Guid id, string name, string issuerNumber)
    {
        return new CardIssuer(
            CardIssuersId.Create(id),
            CardIssuersName.Create(name),
            CardIssuersIssuerNumber.Create(issuerNumber)
        );
    }
}