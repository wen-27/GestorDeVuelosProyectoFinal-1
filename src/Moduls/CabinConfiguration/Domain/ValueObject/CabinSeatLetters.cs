using System;
using System.Linq;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.ValueObject;

public sealed class CabinSeatLetters
{
    public string Value { get; }
    private CabinSeatLetters(string value) => Value = value;

    public static CabinSeatLetters Create(string value, int expectedCount)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Las letras de los asientos son obligatorias.");

        if (value.Length != expectedCount)
            throw new ArgumentException($"La cantidad de letras ({value.Length}) no coincide con los asientos por fila ({expectedCount}).");

        if (value.Any(c => !char.IsLetter(c)))
            throw new ArgumentException("Las letras de los asientos solo pueden contener caracteres alfabéticos.");

        return new CabinSeatLetters(value.ToUpper().Trim());
    }
}