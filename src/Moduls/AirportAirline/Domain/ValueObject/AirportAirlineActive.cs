using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.ValueObject;

public class AirportAirlineActive
{
    public bool Value { get; }
    private AirportAirlineActive(bool value)
    {
        Value = value;
    }

    public static AirportAirlineActive Create(bool value)
    {
        return new AirportAirlineActive(value);
    }
}