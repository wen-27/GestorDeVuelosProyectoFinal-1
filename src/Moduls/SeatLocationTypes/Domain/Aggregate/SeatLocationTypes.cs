using System;
using GestorDeVuelosProyectoFinal.Moduls.SeatLocationTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.SeatLocationTypes.Domain.Aggregate;

public sealed class SeatLocationType
{
    public SeatLocationTypesId Id { get; private set; } = null!;
    public SeatLocationTypesName Name { get; private set; } = null!;

    private SeatLocationType() { }

    private SeatLocationType(SeatLocationTypesId id, SeatLocationTypesName name)
    {
        Id = id;
        Name = name;
    }

    public static SeatLocationType Create(Guid id, string name)
    {
        return new SeatLocationType(
            SeatLocationTypesId.Create(id),
            SeatLocationTypesName.Create(name)
        );
    }

    public void UpdateName(string newName)
    {
        Name = SeatLocationTypesName.Create(newName);
    }
}