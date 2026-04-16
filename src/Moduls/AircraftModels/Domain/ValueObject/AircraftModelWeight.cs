namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelWeight
{
    public decimal? Value { get; }
    private AircraftModelWeight(decimal? value) => Value = value;
    public static AircraftModelWeight Create(decimal? value)
    {
        if (value.HasValue && value < 0)
            throw new ArgumentException("El peso máximo de despegue no puede ser negativo.");
        return new AircraftModelWeight(value);
    }
}