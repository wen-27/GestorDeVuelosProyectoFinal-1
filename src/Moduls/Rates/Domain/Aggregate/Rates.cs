using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Rates.Domain.Aggregate;

public sealed class Rates
{
    public RatesId Id { get; private set; } = null!;
    public RatesBasePrice BasePrice { get; private set; } = null!;
    public RatesValidFrom ValidFrom { get; private set; } = null!;
    public RatesValidTo ValidTo { get; private set; } = null!;

    private Rates() { }

    private Rates(
        RatesId id,
        RatesBasePrice basePrice,
        RatesValidFrom validFrom,
        RatesValidTo validTo)
    {
        Id = id;
        BasePrice = basePrice;
        ValidFrom = validFrom;
        ValidTo = validTo;
    }

    public static Rates Create(
        Guid id,
        decimal basePrice,
        DateOnly validFrom,
        DateOnly validTo)
    {
        return new Rates(
            RatesId.Create(id),
            RatesBasePrice.Create(basePrice),
            RatesValidFrom.Create(validFrom),
            RatesValidTo.Create(validTo)
        );
    }

    public void UpdateBasePrice(decimal newBasePrice)
    {
        // El Value Object RatesBasePrice se encarga de validar (longitud, números, etc.)
        BasePrice = RatesBasePrice.Create(newBasePrice);
    }
}
