namespace GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject; // Todo en minúsculas excepto iniciales

public sealed class AirportsId // Con 't'
{
    public Guid Value { get; }
    private AirportsId(Guid value) => Value = value;
    public static AirportsId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del aeropuerto no es válido", nameof(value));
        return new AirportsId(value);
    }
}