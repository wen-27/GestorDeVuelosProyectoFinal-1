namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;

public sealed class BoardingPassStatus
{
    private static readonly HashSet<string> AllowedStatuses = new(StringComparer.OrdinalIgnoreCase)
    {
        "Generado",
        "Activo",
        "Cancelado",
        "Usado"
    };

    public string Value { get; }

    private BoardingPassStatus(string value) => Value = value;

    public static BoardingPassStatus Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El estado del pase de abordar es obligatorio.", nameof(value));

        var normalized = value.Trim();
        if (!AllowedStatuses.Contains(normalized))
            throw new ArgumentException("El estado del pase de abordar no es valido.", nameof(value));

        return new BoardingPassStatus(normalized);
    }

    public override string ToString() => Value;
}
