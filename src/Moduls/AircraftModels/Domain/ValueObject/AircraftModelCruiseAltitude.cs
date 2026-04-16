namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelCruiseAltitude
{
    public int? Value { get; }
    private AircraftModelCruiseAltitude(int? value) => Value = value;
    public static AircraftModelCruiseAltitude Create(int? value)
    {
        if (value.HasValue && value < 0)
            throw new ArgumentException("La altitud de crucero no puede ser negativa.");
        return new AircraftModelCruiseAltitude(value);
    }
}