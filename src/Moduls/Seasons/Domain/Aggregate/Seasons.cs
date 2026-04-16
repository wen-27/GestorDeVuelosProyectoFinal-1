using System;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.Aggregate;

public sealed class Season
{
    public SeasonsId Id { get; private set; } = null!;
    public SeasonsName Name { get; private set; } = null!;
    public SeasonsDescription Description { get; private set; } = null!;
    public SeasonsPriceFactor PriceFactor { get; private set; } = null!;

    private Season() { }

    public static Season Create(
        Guid id,
        string name,
        string? description,
        decimal priceFactor = 1.0000m)
    {
        return new Season
        {
            Id = SeasonsId.Create(id),
            Name = SeasonsName.Create(name),
            Description = SeasonsDescription.Create(description),
            PriceFactor = SeasonsPriceFactor.Create(priceFactor)
        };
    }

    // Método para actualizar el factor de precio si es necesario
    public void UpdatePriceFactor(decimal newFactor)
    {
        PriceFactor = SeasonsPriceFactor.Create(newFactor);
    }
}