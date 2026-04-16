using System;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Domain.Aggregate;

public sealed class CabinType
{
    public CabinTypesId Id { get; private set; } = null!;
    public CabinTypesName Name { get; private set; } = null!;

    private CabinType() { }

    public static CabinType Create(Guid id, string name)
    {
        return new CabinType
        {
            Id = CabinTypesId.Create(id),
            Name = CabinTypesName.Create(name)
        };
    }
}