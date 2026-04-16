using System;
using GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.Aggregate;

public sealed class Airline
{
    // Identificadores y Datos Básicos
    public AirlinesId Id { get; private set; } = null!;
    public AirlinesName Name { get; private set; } = null!;
    public AirlinesIataCode IataCode { get; private set; } = null!;
    
    // Relación con el módulo de Países
    public CountryId OriginCountryId { get; private set; } = null!;
    
    // Estado y Auditoría
    public AirlinesIsActive IsActive { get; private set; } = null!;
    public AirlinesCreatedIn CreatedIn { get; private set; } = null!;
    public AirlinesUpdatedIn UpdatedIn { get; private set; } = null!;

    // Constructor privado para forzar el uso del método Factory (Create)
    private Airline() { }

    public static Airline Create(
        Guid id, 
        string name, 
        string iataCode, 
        Guid originCountryId, 
        bool isActive,
        DateTime createdIn,
        DateTime updatedIn)
    {
        return new Airline
        {
            Id = AirlinesId.Create(id),
            Name = AirlinesName.Create(name),
            IataCode = AirlinesIataCode.Create(iataCode),
            OriginCountryId = CountryId.Create(originCountryId),
            IsActive = AirlinesIsActive.Create(isActive),
            CreatedIn = AirlinesCreatedIn.Create(createdIn),
            UpdatedIn = AirlinesUpdatedIn.Create(updatedIn)
        };
    }

    // Método para actualizar el estado de la aerolínea (Lógica de Negocio)
    public void UpdateStatus(bool isActive)
    {
        IsActive = AirlinesIsActive.Create(isActive);
        // Aquí podrías actualizar automáticamente el UpdatedIn
    }
}