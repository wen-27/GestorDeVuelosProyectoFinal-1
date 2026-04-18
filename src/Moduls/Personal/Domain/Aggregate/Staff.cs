using System;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.ValueObject; 
using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.Aggregate;

public sealed class Staff 
{
    public PersonalId Id { get; private set; } = null!;
    public PeopleId PersonId { get; private set; } = null!; 
    public PersonalPositionsId PositionId { get; private set; } = null!;
    public AirlinesId? AirlineId { get; private set; }
    public AirportsId? AirportId { get; private set; }
    public PersonalHireDate HireDate { get; private set; } = null!;
    public PersonalIsActive IsActive { get; private set; } = null!;
    public PersonalCreatedIn CreatedIn { get; private set; } = null!;
    public PersonalUpdatedIn UpdatedIn { get; private set; } = null!;

    private Staff() { }

    public static Staff Create(
        int id, int personId, Guid positionId, int? airlineId, int? airportId,
        DateTime hireDate, bool isActive, DateTime createdIn, DateTime updatedIn)
    {
        return new Staff
        {
            Id = PersonalId.Create(id),
            PersonId = PeopleId.Create(personId), 
            PositionId = PersonalPositionsId.Create(positionId),
            AirlineId = airlineId.HasValue ? AirlinesId.Create(airlineId.Value) : null,
            AirportId = airportId.HasValue ? AirportsId.Create(airportId.Value) : null,
            HireDate = PersonalHireDate.Create(hireDate),
            IsActive = PersonalIsActive.Create(isActive),
            CreatedIn = PersonalCreatedIn.Create(createdIn),
            UpdatedIn = PersonalUpdatedIn.Create(updatedIn)
        };
    }
}
