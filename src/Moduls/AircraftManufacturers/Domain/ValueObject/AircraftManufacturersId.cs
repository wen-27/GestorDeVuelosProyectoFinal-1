using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.ValueObject;

public sealed class AircraftManufacturersId
{
    public Guid Value { get; }
    private AircraftManufacturersId(Guid value) => Value = value;

    public static AircraftManufacturersId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del fabricante de aviones no es válido", nameof(value));

        return new AircraftManufacturersId(value);
    }
}
