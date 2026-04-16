using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.ValueObject;

public sealed class SeasonsPriceFactor
{
    public decimal Value { get; }
    private SeasonsPriceFactor(decimal value) => Value = value;

    public static SeasonsPriceFactor Create(decimal value)
    {
        // Validamos que el factor no sea negativo o cero (un vuelo no puede costar $0 o menos)
        if (value <= 0)
            throw new ArgumentException("El factor de precio debe ser un número positivo mayor a cero.");

        return new SeasonsPriceFactor(value);
    }
}