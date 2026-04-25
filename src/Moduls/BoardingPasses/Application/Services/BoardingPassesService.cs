using GestorDeVuelosProyectoFinal.Moduls.Airports.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.People.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.Moduls.Routes.Infrastructure.Entities;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.UseCases;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Checkins.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Flights.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.PassengerReservations.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Passengers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Ticket.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.src.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.Services;

public sealed class BoardingPassesService : IBoardingPassesService
{
    private readonly AppDbContext _db;
    private readonly IBoardingPassesRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public BoardingPassesService(AppDbContext db, IBoardingPassesRepository repository, IUnitOfWork unitOfWork)
    {
        _db = db;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BoardingPass> CreateOrUpdateFromCheckinAsync(int checkinId, CancellationToken cancellationToken = default)
    {
        var checkin = await _db.Checkins.FirstOrDefaultAsync(x => x.Id == checkinId, cancellationToken)
            ?? throw new InvalidOperationException("Check-in no encontrado.");

        var ticket = await _db.Tickets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == checkin.TicketId, cancellationToken)
            ?? throw new InvalidOperationException("Tiquete no encontrado.");

        var flight = await ResolveFlightForTicketAsync(ticket.Id, cancellationToken)
            ?? throw new InvalidOperationException("No se pudo resolver el vuelo del tiquete.");

        var seat = await _db.FlightSeats.AsNoTracking().FirstOrDefaultAsync(x => x.Id == checkin.FlightSeatId, cancellationToken)
            ?? throw new InvalidOperationException("Asiento no encontrado.");

        var boardingAt = BoardingPassBoardingAt.Create(flight.DepartureAt.AddMinutes(-45));
        var gate = BoardingPassGate.Create(GenerateGate(flight.Id));
        var seatCode = BoardingPassSeatCode.Create(seat.Code);
        var status = BoardingPassStatus.Create("Activo");
        var code = BoardingPassCode.Create(
            string.IsNullOrWhiteSpace(checkin.BoardingPassNumber)
                ? $"PASE-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}"
                : checkin.BoardingPassNumber);
        var existing = await new CreateOrUpdateBoardingPassUseCase(_repository).GetExistingByCheckinAsync(checkinId, cancellationToken);

        if (existing is null)
        {
            var entity = new BoardingPassEntity
            {
                Code = code.Value,
                CheckinId = checkin.Id,
                TicketId = ticket.Id,
                FlightId = flight.Id,
                Gate = gate.Value,
                SeatCode = seatCode.Value,
                BoardingAt = boardingAt.Value,
                Status = status.Value,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _db.BoardingPasses.Add(entity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return await BuildResultByEntityIdAsync(entity.Id, cancellationToken)
                ?? throw new InvalidOperationException("No se pudo construir el pase de abordar.");
        }

        var existingEntity = await _db.BoardingPasses.FirstOrDefaultAsync(x => x.Id == existing.Id.Value, cancellationToken)
            ?? throw new InvalidOperationException("No se encontro el pase de abordar a actualizar.");

        existingEntity.Gate = gate.Value;
        existingEntity.SeatCode = seatCode.Value;
        existingEntity.BoardingAt = boardingAt.Value;
        existingEntity.Status = status.Value;
        existingEntity.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return await BuildResultByEntityIdAsync(existingEntity.Id, cancellationToken)
            ?? throw new InvalidOperationException("No se pudo construir el pase de abordar.");
    }

    public Task<BoardingPass?> GetByBoardingPassCodeAsync(string boardingPassCode, CancellationToken cancellationToken = default)
        => new GetBoardingPassByBoardingPassCodeUseCase(_repository).ExecuteAsync(boardingPassCode, cancellationToken);

    public Task<BoardingPass?> GetByTicketCodeAsync(string ticketCode, CancellationToken cancellationToken = default)
        => new GetBoardingPassByTicketCodeUseCase(_repository).ExecuteAsync(ticketCode, cancellationToken);

    public Task<BoardingPass?> GetByDocumentAsync(string documentNumber, CancellationToken cancellationToken = default)
        => new GetBoardingPassByDocumentUseCase(_repository).ExecuteAsync(documentNumber, cancellationToken);

    public Task<BoardingPass?> GetByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default)
        => new GetBoardingPassByTicketIdUseCase(_repository).ExecuteAsync(ticketId, cancellationToken);

    public async Task<IReadOnlyList<ReadyBoardingFlightDto>> GetFlightsWithReadyPassengersAsync(CancellationToken cancellationToken = default)
    {
        var rows = await new GetFlightsWithReadyPassengersUseCase(_repository).ExecuteAsync(cancellationToken);
        return rows.ToList();
    }

    public async Task<IReadOnlyList<ReadyToBoardPassengerDto>> GetReadyToBoardByFlightAsync(int flightId, CancellationToken cancellationToken = default)
    {
        var rows = await new GetReadyToBoardByFlightUseCase(_repository).ExecuteAsync(flightId, cancellationToken);
        return rows.ToList();
    }

    private async Task<BoardingPass?> BuildResultByEntityIdAsync(int boardingPassId, CancellationToken cancellationToken)
    {
        var row = await (
            from bp in _db.BoardingPasses.AsNoTracking()
            join ticket in _db.Tickets.AsNoTracking() on bp.TicketId equals ticket.Id
            join flight in _db.Flights.AsNoTracking() on bp.FlightId equals flight.Id
            join route in _db.Routes.AsNoTracking() on flight.RouteId equals route.Id
            join ao in _db.Airports.AsNoTracking() on route.OriginAirportId equals ao.Id
            join ad in _db.Airports.AsNoTracking() on route.DestinationAirportId equals ad.Id
            join pr in _db.PassengerReservations.AsNoTracking() on ticket.PassengerReservation_Id equals pr.Id
            join passenger in _db.Passengers.AsNoTracking() on pr.Passenger_Id equals passenger.Id
            join person in _db.Persons.AsNoTracking() on passenger.PersonId equals person.Id
            where bp.Id == boardingPassId
            select new
            {
                bp.Id,
                bp.CheckinId,
                bp.TicketId,
                bp.FlightId,
                bp.Code,
                TicketCode = ticket.Code,
                PassengerName = person.FirstName + " " + person.LastName,
                PassengerDocument = person.DocumentNumber,
                flight.FlightCode,
                RouteLabel = $"{ao.IataCode} → {ad.IataCode}",
                bp.SeatCode,
                bp.Gate,
                flight.DepartureAt,
                bp.BoardingAt,
                bp.Status
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (row is null)
            return null;

        return BoardingPass.Create(
            row.Id,
            row.CheckinId,
            row.TicketId,
            row.FlightId,
            row.Code,
            row.TicketCode,
            row.PassengerName,
            row.PassengerDocument,
            row.FlightCode,
            row.RouteLabel,
            row.SeatCode,
            row.Gate,
            row.DepartureAt,
            row.BoardingAt,
            row.Status);
    }

    private async Task<FlightEntity?> ResolveFlightForTicketAsync(int ticketId, CancellationToken cancellationToken)
    {
        var ticket = await _db.Tickets.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ticketId, cancellationToken);
        if (ticket is null)
            return null;

        var pr = await _db.PassengerReservations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == ticket.PassengerReservation_Id, cancellationToken);
        if (pr is null)
            return null;

        var fr = await _db.FlightReservations.AsNoTracking().FirstOrDefaultAsync(x => x.Id == pr.Flight_Reservation_Id, cancellationToken);
        if (fr is null)
            return null;

        var bf = await _db.BookingFlights.AsNoTracking().FirstOrDefaultAsync(x => x.Id == fr.BookingFlightId, cancellationToken);
        if (bf is null)
            return null;

        return await _db.Flights.FirstOrDefaultAsync(x => x.Id == bf.FlightId, cancellationToken);
    }

    private static string GenerateGate(int flightId)
    {
        var letter = (char)('A' + (flightId % 6));
        var number = 1 + (flightId % 24);
        return $"{letter}{number}";
    }
}
