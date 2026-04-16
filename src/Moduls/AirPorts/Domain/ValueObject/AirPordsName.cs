namespace GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject;

public sealed class AirportsName // Cambiado de AirPords a Airports
{
    public string Value { get; }
    private AirportsName(string value) => Value = value;

    public static AirportsName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El nombre del aeropuerto no puede estar vacío", nameof(value));

        return new AirportsName(value.Trim());
    }
}