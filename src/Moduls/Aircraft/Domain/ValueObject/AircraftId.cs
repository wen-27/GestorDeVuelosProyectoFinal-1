using System;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.ValueObject;

public sealed class AircraftId
{
    public Guid Value { get; }
    private AircraftId(Guid value) => Value = value;

    public static AircraftId Create(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("El id del avión no es válido", nameof(value));

        return new AircraftId(value);
    }
}
