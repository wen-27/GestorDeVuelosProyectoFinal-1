using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

public sealed class AircraftDateManufacture
{
    public DateTime Value { get; }
    private AircraftDateManufacture(DateTime value) => Value = value;

    public static AircraftDateManufacture Create(DateTime value)
    {
        if (value == DateTime.MinValue)
            throw new ArgumentException("El campo date_manufacture no puede ser igual a DateTime.MinValue");

        return new AircraftDateManufacture(value);
    }
}
