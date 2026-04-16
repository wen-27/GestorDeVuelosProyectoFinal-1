using System;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.Aggregate;

// CAMBIO: Renombrado de StaffAvailability a StaffAvailabilityRecord
public sealed class StaffAvailabilityRecord 
{
    public StaffAvailabilityId Id { get; private set; } = null!;
    public PersonalId StaffId { get; private set; } = null!;
    public AvailabilityStatesId StateId { get; private set; } = null!;
    public StaffAvailabilityDates Dates { get; private set; } = null!;
    public StaffAvailabilityObservation Observation { get; private set; } = null!;

    private StaffAvailabilityRecord() { }

    public static StaffAvailabilityRecord Create(
        Guid id,
        Guid staffId,
        Guid stateId,
        DateTime startDate,
        DateTime endDate,
        string? observation)
    {
        return new StaffAvailabilityRecord
        {
            Id = StaffAvailabilityId.Create(id),
            StaffId = PersonalId.Create(staffId),
            StateId = AvailabilityStatesId.Create(stateId),
            Dates = StaffAvailabilityDates.Create(startDate, endDate),
            Observation = StaffAvailabilityObservation.Create(observation)
        };
    }
}