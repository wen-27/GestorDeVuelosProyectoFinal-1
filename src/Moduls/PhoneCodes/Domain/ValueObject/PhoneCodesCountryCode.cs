using System;

namespace GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Domain.ValueObject;

public sealed record PhoneCodesCountryCode
{
    public string Value { get; }
    private PhoneCodesCountryCode(string value) => Value = value;

    public static PhoneCodesCountryCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El código de país es obligatorio.");

        var formatted = value.Trim();

        if (!formatted.StartsWith("+"))
            throw new ArgumentException("El código de país debe iniciar con el símbolo '+'.");

        if (formatted.Length > 5)
            throw new ArgumentOutOfRangeException(nameof(value), "El código de país no puede superar los 5 caracteres.");

        return new PhoneCodesCountryCode(formatted);
    }
}