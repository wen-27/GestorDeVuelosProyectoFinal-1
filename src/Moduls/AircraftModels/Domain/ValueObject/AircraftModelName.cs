namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelName
{
    public string Value { get; }
    private AircraftModelName(string value) => Value = value;
    public static AircraftModelName Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("El nombre del modelo es obligatorio.");
        return new AircraftModelName(value.Trim());
    }
}