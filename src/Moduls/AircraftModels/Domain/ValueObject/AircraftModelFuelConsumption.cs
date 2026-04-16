namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelFuelConsumption
{
    public decimal? Value { get; }
    private AircraftModelFuelConsumption(decimal? value) => Value = value;
    public static AircraftModelFuelConsumption Create(decimal? value)
    {
        if (value.HasValue && value < 0)
            throw new ArgumentException("El consumo de combustible no puede ser negativo.");
        return new AircraftModelFuelConsumption(value);
    }
}