using System;

namespace GestorDeVuelosProyectoFinal.Moduls.AircraftModels.Domain.ValueObject;

public sealed class AircraftModelId
{
    public Guid Value { get; }
    private AircraftModelId(Guid value) => Value = value;
    public static AircraftModelId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID del modelo no es válido.");
        return new AircraftModelId(value);
    }
}