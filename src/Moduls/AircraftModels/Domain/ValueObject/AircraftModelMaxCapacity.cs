namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelMaxCapacity
{
    public int Value { get; }
    private AircraftModelMaxCapacity(int value) => Value = value;
    public static AircraftModelMaxCapacity Create(int value)
    {
        if (value <= 0)
            throw new ArgumentException("La capacidad máxima debe ser mayor a cero.");
        return new AircraftModelMaxCapacity(value);
    }
}