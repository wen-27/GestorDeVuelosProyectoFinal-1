using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.ValueObject;

public sealed class AirportAirlineId
{
    public Guid Value { get; }
    private AirportAirlineId(Guid value) => Value = value;

    public static AirportAirlineId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del aeropuerto-aerolínea no es válido", nameof(value));

        return new AirportAirlineId(value);
    }
}
