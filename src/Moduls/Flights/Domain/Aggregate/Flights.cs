using System;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.ValueObject;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Flights.Domain.Aggregate;

public sealed class Flights
{
    public FlightsId Id { get; private set; } = null!;
    public FlightCode Code { get; private set; } = null!;
    public FlightDepartureTime DepartureTime { get; private set; } = null!;
    public FlightEstimatedArrivalTime EstimatedArrivalTime { get; private set; } = null!;
    public FlightTotalCapacity TotalCapacity { get; private set; } = null!;
    public FlightAvailableSeats AvailableSeats { get; private set; } = null!;
    public FlightRescheduledAt RescheduledAt { get; private set; } = null!;
    public FlightsCreatedAt CreatedAt { get; private set; } = null!;
    public FlightUpdatedAt UpdatedAt { get; private set; } = null!;
    

    private Flights() { }

    private Flights(
        FlightsId id,
        FlightCode code,
        FlightDepartureTime departureTime,
        FlightEstimatedArrivalTime estimatedArrivalTime,
        FlightTotalCapacity totalCapacity,
        FlightAvailableSeats availableSeats,
        FlightRescheduledAt rescheduledAt,
        FlightsCreatedAt createdAt,
        FlightUpdatedAt updatedAt)
    {
        Id = id;
        Code = code;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        RescheduledAt = rescheduledAt;
        EstimatedArrivalTime = estimatedArrivalTime;
        DepartureTime = departureTime;
        TotalCapacity = totalCapacity;
        AvailableSeats = availableSeats;
    }

    public static Flights Create(
        Guid id,
        string code,
        DateTime departureTime,
        DateTime estimatedArrivalTime,
        int totalCapacity,
        int availableSeats,
        DateTime createdAt,
        DateTime updatedAt,
        DateTime rescheduledAt       
        )
    {
        return new Flights(
            FlightsId.Create(id),
            FlightCode.Create(code),
            FlightDepartureTime.Create(departureTime),
            FlightEstimatedArrivalTime.Create(estimatedArrivalTime),
            FlightTotalCapacity.Create(totalCapacity),
            FlightAvailableSeats.Create(availableSeats),
            FlightRescheduledAt.Create(rescheduledAt),
            FlightsCreatedAt.Create(createdAt),
            FlightUpdatedAt.Create(updatedAt)    
        );
    }

    public void UpdateCode(string newCode)
    {
        // El Value Object FlightCode se encarga de validar (longitud, números, etc.)
        Code = FlightCode.Create(newCode);
    }

    public void UpdateDepartureTime(DateTime newDepartureTime)
    {
        // El Value Object FlightDepartureTime se encarga de validar (fecha no vacia, fecha en el pasado, etc.)
        DepartureTime = FlightDepartureTime.Create(newDepartureTime);
    }

    public void UpdateEstimatedArrivalTime(DateTime newEstimatedArrivalTime)
    {
        // El Value Object FlightEstimatedArrivalTime se encarga de validar (fecha no vacia, fecha en el pasado, etc.)
        EstimatedArrivalTime = FlightEstimatedArrivalTime.Create(newEstimatedArrivalTime);
    }

    public void UpdateTotalCapacity(int newTotalCapacity)
    {
        // El Value Object FlightTotalCapacity se encarga de validar (longitud, números, etc.)
        TotalCapacity = FlightTotalCapacity.Create(newTotalCapacity);
    }

    public void UpdateAvailableSeats(int newAvailableSeats)
    {
        // El Value Object FlightAvailableSeats se encarga de validar (longitud, números, etc.)
        AvailableSeats = FlightAvailableSeats.Create(newAvailableSeats);
    }
}
