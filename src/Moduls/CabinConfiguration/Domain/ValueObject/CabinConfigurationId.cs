using System;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.ValueObject;

public sealed class CabinConfigurationId
{
    public Guid Value { get; }
    private CabinConfigurationId(Guid value) => Value = value;
    public static CabinConfigurationId Create(Guid value)
    {
        if (value == Guid.Empty) throw new ArgumentException("El ID de configuración no es válido.");
        return new CabinConfigurationId(value);
    }
}