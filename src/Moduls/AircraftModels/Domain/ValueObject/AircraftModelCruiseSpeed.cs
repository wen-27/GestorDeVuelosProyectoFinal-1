namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelCruiseSpeed
{
    public int? Value { get; }
    private AircraftModelCruiseSpeed(int? value) => Value = value;
    public static AircraftModelCruiseSpeed Create(int? value)
    {
        if (value.HasValue && value < 0)
            throw new ArgumentException("La velocidad de crucero no puede ser negativa.");
        return new AircraftModelCruiseSpeed(value);
    }
}