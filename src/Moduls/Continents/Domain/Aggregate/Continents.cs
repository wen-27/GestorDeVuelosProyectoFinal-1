using System;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Continents.Domain.Aggregate;

public sealed class Continent
{
    // Propiedades con set privado para garantizar el encapsulamiento
    public ContinentsId Id { get; private set; } = null!;
    public ContinentName Name { get; private set; } = null!;

    // Constructor vacío para ORMs como Entity Framework (necesario a veces)
    private Continent() { }

    // Constructor principal privado
    private Continent(ContinentsId id, ContinentName name)
    {
        Id = id;
        Name = name;
    }


    public static Continent Create(Guid id, string name)
    {
        return new Continent(
            ContinentsId.Create(id),
            ContinentName.Create(name)
        );
    }


    public void UpdateName(string newName)
    {
        // El Value Object ContinentName se encarga de validar (longitud, números, etc.)
        Name = ContinentName.Create(newName);
    }
}