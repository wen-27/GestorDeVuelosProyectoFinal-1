using System.Text.RegularExpressions;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

public sealed class BoardingPassGate
{
    public string Value { get; }

    private BoardingPassGate(string value) => Value = value;

    public static BoardingPassGate Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("La puerta de abordaje es obligatoria.", nameof(value));

        var normalized = value.Trim().ToUpperInvariant();
        if (normalized.Length > 20)
            throw new ArgumentException("La puerta de abordaje no puede superar los 20 caracteres.", nameof(value));

        if (!Regex.IsMatch(normalized, @"^[A-Z0-9\-]+$"))
            throw new ArgumentException("La puerta de abordaje tiene un formato invalido.", nameof(value));

        return new BoardingPassGate(normalized);
    }

    public override string ToString() => Value;
}
