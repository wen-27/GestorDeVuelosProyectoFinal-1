using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.DTOs;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Repositories;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.ValueObject;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using Microsoft.EntityFrameworkCore;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Infrastructure.Repository;

public sealed class BoardingPassesRepository : IBoardingPassesRepository
{
    private readonly AppDbContext _context;

    public BoardingPassesRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<BoardingPass?> FindByIdAsync(BoardingPassId id) => FindOneAsync(x => x.Id == id.Value);

    public Task<IEnumerable<BoardingPass>> FindAllAsync() => FindManyAsync();

    public Task<BoardingPass?> FindByCheckinIdAsync(int checkinId) => FindOneAsync(x => x.CheckinId == checkinId);

    public Task<BoardingPass?> FindByTicketIdAsync(int ticketId) => FindOneAsync(x => x.TicketId == ticketId);

    public Task<BoardingPass?> FindByTicketCodeAsync(string ticketCode)
        => FindOneAsync(x => x.Ticket.Code == ticketCode.Trim());

    public Task<BoardingPass?> FindByBoardingPassCodeAsync(string boardingPassCode)
        => FindOneAsync(x => x.Code == boardingPassCode.Trim().ToUpperInvariant());

    public Task<BoardingPass?> FindByDocumentAsync(string documentNumber)
        => FindOneAsync(x => x.Ticket.PassengerReservation!.Passenger!.Person!.DocumentNumber == documentNumber.Trim());

    public async Task<IEnumerable<ReadyBoardingFlightDto>> FindFlightsWithReadyPassengersAsync()
    {
        var rows = await (
            from bp in _context.BoardingPasses.AsNoTracking()
            join flight in _context.Flights.AsNoTracking() on bp.FlightId equals flight.Id
            join route in _context.Routes.AsNoTracking() on flight.RouteId equals route.Id
            join ao in _context.Airports.AsNoTracking() on route.OriginAirportId equals ao.Id
            join ad in _context.Airports.AsNoTracking() on route.DestinationAirportId equals ad.Id
            where bp.Status == "Activo"
            select new
            {
                flight.Id,
                flight.FlightCode,
                RouteLabel = ao.IataCode + " → " + ad.IataCode,
                flight.DepartureAt
            })
            .Distinct()
            .OrderBy(x => x.DepartureAt)
            .ToListAsync();

        return rows.Select(x => new ReadyBoardingFlightDto(x.Id, x.FlightCode, x.RouteLabel, x.DepartureAt));
    }

    public async Task<IEnumerable<ReadyToBoardPassengerDto>> FindReadyToBoardByFlightAsync(int flightId)
    {
        var rows = await (
            from bp in _context.BoardingPasses.AsNoTracking()
            join checkin in _context.Checkins.AsNoTracking() on bp.CheckinId equals checkin.Id
            join ticket in _context.Tickets.AsNoTracking() on bp.TicketId equals ticket.Id
            join pr in _context.PassengerReservations.AsNoTracking() on ticket.PassengerReservation_Id equals pr.Id
            join passenger in _context.Passengers.AsNoTracking() on pr.Passenger_Id equals passenger.Id
            join person in _context.Persons.AsNoTracking() on passenger.PersonId equals person.Id
            join ticketState in _context.TicketStates.AsNoTracking() on ticket.TicketState_Id equals ticketState.Id
            join flight in _context.Flights.AsNoTracking() on bp.FlightId equals flight.Id
            where bp.FlightId == flightId && bp.Status == "Activo"
            orderby bp.SeatCode, checkin.CheckedInAt
            select new
            {
                flight.Id,
                flight.FlightCode,
                PassengerName = person.FirstName + " " + person.LastName,
                person.DocumentNumber,
                bp.SeatCode,
                TicketCode = ticket.Code,
                TicketState = ticketState.Name,
                checkin.CheckedInAt
            })
            .ToListAsync();

        return rows.Select(x => new ReadyToBoardPassengerDto(
            x.Id,
            x.FlightCode,
            x.PassengerName,
            x.DocumentNumber,
            x.SeatCode,
            x.TicketCode,
            x.TicketState,
            x.CheckedInAt));
    }

    public async Task SaveAsync(BoardingPass boardingPass)
    {
        var entity = MapToEntity(boardingPass);
        await _context.Set<BoardingPassEntity>().AddAsync(entity);
    }

    public async Task UpdateAsync(BoardingPass boardingPass)
    {
        var entity = await _context.Set<BoardingPassEntity>()
            .FirstOrDefaultAsync(x => x.Id == boardingPass.Id.Value);

        if (entity is null) return;

        entity.Code = boardingPass.Code.Value;
        entity.Gate = boardingPass.Gate.Value;
        entity.SeatCode = boardingPass.SeatCode.Value;
        entity.BoardingAt = boardingPass.BoardingAt.Value;
        entity.Status = boardingPass.Status.Value;
        entity.UpdatedAt = DateTime.UtcNow;

        _context.Set<BoardingPassEntity>().Update(entity);
    }

    public async Task DeleteAsync(BoardingPassId id)
    {
        var entity = await _context.Set<BoardingPassEntity>()
            .FirstOrDefaultAsync(x => x.Id == id.Value);

        if (entity is null) return;

        _context.Set<BoardingPassEntity>().Remove(entity);
    }

    private async Task<BoardingPass?> FindOneAsync(System.Linq.Expressions.Expression<Func<BoardingPassEntity, bool>> predicate)
    {
        var row = await (
            from bp in _context.BoardingPasses.AsNoTracking().Where(predicate)
            join ticket in _context.Tickets.AsNoTracking() on bp.TicketId equals ticket.Id
            join flight in _context.Flights.AsNoTracking() on bp.FlightId equals flight.Id
            join route in _context.Routes.AsNoTracking() on flight.RouteId equals route.Id
            join ao in _context.Airports.AsNoTracking() on route.OriginAirportId equals ao.Id
            join ad in _context.Airports.AsNoTracking() on route.DestinationAirportId equals ad.Id
            join pr in _context.PassengerReservations.AsNoTracking() on ticket.PassengerReservation_Id equals pr.Id
            join passenger in _context.Passengers.AsNoTracking() on pr.Passenger_Id equals passenger.Id
            join person in _context.Persons.AsNoTracking() on passenger.PersonId equals person.Id
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
                RouteLabel = ao.IataCode + " → " + ad.IataCode,
                bp.SeatCode,
                bp.Gate,
                flight.DepartureAt,
                bp.BoardingAt,
                bp.Status
            })
            .FirstOrDefaultAsync();

        return row is null ? null : MapToDomain(row);
    }

    private async Task<IEnumerable<BoardingPass>> FindManyAsync()
    {
        var rows = await (
            from bp in _context.BoardingPasses.AsNoTracking()
            join ticket in _context.Tickets.AsNoTracking() on bp.TicketId equals ticket.Id
            join flight in _context.Flights.AsNoTracking() on bp.FlightId equals flight.Id
            join route in _context.Routes.AsNoTracking() on flight.RouteId equals route.Id
            join ao in _context.Airports.AsNoTracking() on route.OriginAirportId equals ao.Id
            join ad in _context.Airports.AsNoTracking() on route.DestinationAirportId equals ad.Id
            join pr in _context.PassengerReservations.AsNoTracking() on ticket.PassengerReservation_Id equals pr.Id
            join passenger in _context.Passengers.AsNoTracking() on pr.Passenger_Id equals passenger.Id
            join person in _context.Persons.AsNoTracking() on passenger.PersonId equals person.Id
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
                RouteLabel = ao.IataCode + " → " + ad.IataCode,
                bp.SeatCode,
                bp.Gate,
                flight.DepartureAt,
                bp.BoardingAt,
                bp.Status
            })
            .ToListAsync();

        return rows.Select(MapToDomain);
    }

    private static BoardingPass MapToDomain(dynamic row)
    {
        return BoardingPass.Create(
            (int)row.Id,
            (int)row.CheckinId,
            (int)row.TicketId,
            (int)row.FlightId,
            (string)row.Code,
            (string)row.TicketCode,
            (string)row.PassengerName,
            (string)row.PassengerDocument,
            (string)row.FlightCode,
            (string)row.RouteLabel,
            (string)row.SeatCode,
            (string)row.Gate,
            (DateTime)row.DepartureAt,
            (DateTime)row.BoardingAt,
            (string)row.Status);
    }

    private static BoardingPassEntity MapToEntity(BoardingPass domain)
    {
        return new BoardingPassEntity
        {
            CheckinId = domain.CheckinId,
            TicketId = domain.TicketId,
            FlightId = domain.FlightId,
            Code = domain.Code.Value,
            Gate = domain.Gate.Value,
            SeatCode = domain.SeatCode.Value,
            BoardingAt = domain.BoardingAt.Value,
            Status = domain.Status.Value,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
