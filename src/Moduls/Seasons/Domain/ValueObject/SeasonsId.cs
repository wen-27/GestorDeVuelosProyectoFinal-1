using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.ValueObject;

public sealed class SeasonsId
{
    public Guid Value { get; }
    private SeasonsId(Guid value) => Value = value;
    public static SeasonsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id de la temporada no es válido", nameof(value));

        return new SeasonsId(value);
    }
}