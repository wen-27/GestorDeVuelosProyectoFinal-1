using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AirportAirline.Domain.ValueObject;

public class AirportAirlineStartDate
{
    public DateTime Value { get; }
    private AirportAirlineStartDate(DateTime value) => Value = value;

    public static AirportAirlineStartDate Create(DateTime value)
    {
        if (value == DateTime.MinValue)
            throw new ArgumentException("El campo start_date no puede ser igual a DateTime.MinValue");

        return new AirportAirlineStartDate(value);
    }
}
