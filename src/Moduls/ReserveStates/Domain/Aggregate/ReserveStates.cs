using System;
using GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.ReserveStates.Domain.Aggregate;

public sealed class ReserveStates
{
    // Propiedades con set privado para garantizar el encapsulamiento
    public ReserveStateId Id { get; private set; } = null!;
    public ReserveStateName Name { get; private set; } = null!;

    // Constructor vacío para ORMs como Entity Framework (necesario a veces)
    private ReserveStates() { }

    // Constructor principal privado
    private ReserveStates(
        ReserveStateId id,
        ReserveStateName name)
    {
        Id = id;
        Name = name;
    }

    public static ReserveStates Create(
        Guid id,
        string name)
    {
        return new ReserveStates(
            ReserveStateId.Create(id),
            ReserveStateName.Create(name)
        );
    }

    public void UpdateName(string newName)
    {
        // El Value Object ReserveStateName se encarga de validar (longitud, números, etc.)
        Name = ReserveStateName.Create(newName);
    }
}
