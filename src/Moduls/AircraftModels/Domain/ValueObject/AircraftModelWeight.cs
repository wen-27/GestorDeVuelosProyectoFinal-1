namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed record AircraftModelWeight
{
    public decimal? Value { get; }
    private AircraftModelWeight(decimal? value) => Value = value;
    public static AircraftModelWeight Create(decimal? value)
    {
        if (value.HasValue && value < 0)
            throw new ArgumentException("El peso máximo de despegue no puede ser negativo.", nameof(value));
        if (value.HasValue && value > 50000)
            throw new ArgumentException("El peso máximo de despegue no puede superar 10000 Kg.", nameof(value));
        return new AircraftModelWeight(value);
    }
    public override string ToString() => Value?.ToString() ?? "N/A";
}