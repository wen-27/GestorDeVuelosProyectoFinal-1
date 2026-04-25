using System.Text.RegularExpressions;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

public sealed class BoardingPassCode
{
    public string Value { get; }

    private BoardingPassCode(string value) => Value = value;

    public static BoardingPassCode Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El codigo del pase de abordar es obligatorio.", nameof(value));

        var normalized = value.Trim().ToUpperInvariant();
        if (normalized.Length > 30)
            throw new ArgumentException("El codigo del pase de abordar no puede superar los 30 caracteres.", nameof(value));

        if (!Regex.IsMatch(normalized, @"^[A-Z0-9\-]+$"))
            throw new ArgumentException("El codigo del pase de abordar contiene caracteres no permitidos.", nameof(value));

        return new BoardingPassCode(normalized);
    }

    public override string ToString() => Value;
}
