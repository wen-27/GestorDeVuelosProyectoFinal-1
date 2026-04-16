using System;

namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.ValueObject;

public sealed record AirlinesIataCode
{
    public string Value { get; }
    private AirlinesIataCode(string value) => Value = value;

    public static AirlinesIataCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código IATA es obligatorio.");

        var code = value.Trim().ToUpper();
        if (code.Length < 2 || code.Length > 3)
            throw new ArgumentException("El código IATA debe tener entre 2 y 3 caracteres.");

        return new AirlinesIataCode(code);
    }
}