using System;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Aggregate;

public sealed class PassengerType
{
    public PassengerTypesId Id { get; private set; } = null!;
    public PassengerTypesName Name { get; private set; } = null!;
    public PassengerMinAge MinAge { get; private set; } = null!;
    public PassengerMaxAge MaxAge { get; private set; } = null!;

    private PassengerType() { }

    public static PassengerType Create(Guid id, string name, int minAge, int maxAge)
    {
        if (minAge > maxAge)
            throw new ArgumentException($"Error en '{name}': la edad mínima no puede superar a la máxima.");

        return new PassengerType
        {
            Id = PassengerTypesId.Create(id),
            Name = PassengerTypesName.Create(name),
            MinAge = PassengerMinAge.Create(minAge),
            MaxAge = PassengerMaxAge.Create(maxAge)
        };
    }
}