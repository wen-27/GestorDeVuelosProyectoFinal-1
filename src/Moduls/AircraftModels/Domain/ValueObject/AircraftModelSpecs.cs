namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelSpecs
{
    public int MaxCapacity { get; }
    public decimal? MaxTakeoffWeight { get; }
    public decimal? FuelConsumption { get; }

    private AircraftModelSpecs(int capacity, decimal? weight, decimal? consumption)
    {
        MaxCapacity = capacity;
        MaxTakeoffWeight = weight;
        FuelConsumption = consumption;
    }

    public static AircraftModelSpecs Create(int capacity, decimal? weight, decimal? consumption)
    {
        if (capacity <= 0) throw new ArgumentException("La capacidad debe ser mayor a cero.");
        if (weight < 0 || consumption < 0) throw new ArgumentException("Los valores técnicos no pueden ser negativos.");
        
        return new AircraftModelSpecs(capacity, weight, consumption);
    }
}