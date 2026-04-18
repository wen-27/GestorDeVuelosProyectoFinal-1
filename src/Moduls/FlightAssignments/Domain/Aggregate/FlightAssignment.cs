using System;
using GestorDeVuelosProyectoFinal.Moduls.FlightAssignments.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.FlightRoles.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.Moduls.FlightAssignments.Domain.Aggregate;

public sealed class FlightAssignment
{
    public FlightAssignmentId Id { get; private set; } = null!;
    public FlightsId FlightId { get; private set; } = null!; 
    public StaffAvailabilityId StaffId { get; private set; } = null!;
    public FlightRolesId FlightRoleId { get; private set; } = null!;

    private FlightAssignment() { }

    public static FlightAssignment Create(int id, int flightId, int staffId, int flightRoleId)
    {
        return new FlightAssignment
        {
            Id = FlightAssignmentId.Create(id),
            FlightId = FlightsId.Create(flightId), 
            StaffId = StaffAvailabilityId.Create(staffId),
            FlightRoleId = FlightRolesId.Create(flightRoleId)
        };
    }
}
