using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.ClientPortal.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.ClientPortal.Application.Models;
using GestorDeVuelosProyectoFinal.src.Moduls.ClientPortal.Application.Support;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Checkins.UI;

public sealed class CheckinsConsoleUI
{
    private readonly IClientPortalService _clientPortalService;
    private readonly IBoardingPassesService _boardingPassesService;
    private readonly AppDbContext _db;

    public CheckinsConsoleUI(
        IClientPortalService clientPortalService,
        IBoardingPassesService boardingPassesService,
        AppDbContext db)
    {
        _clientPortalService = clientPortalService;
        _boardingPassesService = boardingPassesService;
        _db = db;
    }

    public async Task ShowAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new FigletText("Check-in").Color(Color.Blue));

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold yellow]Selecciona una opción:[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "Realizar check-in",
                        "Consultar pase de abordar",
                        "Consultar pasajeros listos para abordar",
                        "Volver"));

            switch (option)
            {
                case "Realizar check-in":
                    await PerformCheckinAsync(cancellationToken);
                    break;
                case "Consultar pase de abordar":
                    await QueryBoardingPassAsync(cancellationToken);
                    break;
                case "Consultar pasajeros listos para abordar":
                    await QueryReadyPassengersAsync(cancellationToken);
                    break;
                default:
                    return;
            }
        }
    }

    private async Task PerformCheckinAsync(CancellationToken cancellationToken)
    {
        try
        {
            var target = await ResolveTargetAsync(cancellationToken);
            if (target is null)
                return;

            while (!cancellationToken.IsCancellationRequested)
            {
                var candidates = await _clientPortalService.GetCheckinCandidatesAsync(
                    target.ClientId,
                    target.BookingId,
                    target.FlightId,
                    DateTime.UtcNow,
                    cancellationToken);

                AnsiConsole.Clear();
                AnsiConsole.Write(new Rule("[bold deepskyblue1] Realizar check-in [/]").LeftJustified());
                AnsiConsole.MarkupLine($"[grey]Reserva:[/] [white]{Markup.Escape(ReservationReferenceCodec.Encode(target.BookingId))}[/]");
                AnsiConsole.MarkupLine($"[grey]Vuelo:[/] [white]{Markup.Escape(candidates.FlightCode)}[/]  [grey]Ruta:[/] [white]{Markup.Escape(candidates.OriginIata)} → {Markup.Escape(candidates.DestinationIata)}[/]");
                AnsiConsole.MarkupLine($"[grey]Salida:[/] [white]{candidates.DepartureAt:yyyy-MM-dd HH:mm}[/]  [grey]Llegada:[/] [white]{candidates.ArrivalAt:yyyy-MM-dd HH:mm}[/]\n");

                RenderPassengersTable("Pendientes por check-in", candidates.PendingPassengers, false);
                RenderPassengersTable("Check-in realizado", candidates.CheckedInPassengers, true);

                if (candidates.PendingPassengers.Count == 0)
                {
                    AnsiConsole.MarkupLine("\n[green]Todos los pasajeros de este vuelo ya están listos para abordar.[/]");
                    Pause();
                    return;
                }

                var action = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\n¿Qué deseas hacer?")
                        .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                        .AddChoices("Hacer check-in a un pasajero", "Hacer check-in a todos", "Volver"));

                if (action == "Volver")
                    return;

                if (action == "Hacer check-in a todos")
                {
                    if (!AnsiConsole.Confirm("Se asignará asiento aleatorio a cada pasajero pendiente. ¿Deseas continuar?", true))
                        continue;

                    var completedTable = new Table()
                        .Border(TableBorder.Rounded)
                        .BorderColor(Color.Grey)
                        .AddColumn("[bold grey]Pasajero[/]")
                        .AddColumn("[bold grey]Asiento[/]")
                        .AddColumn("[bold grey]Puerta[/]")
                        .AddColumn("[bold grey]Hora abordaje[/]")
                        .AddColumn("[bold grey]Pase[/]");

                    foreach (var passenger in candidates.PendingPassengers)
                    {
                        var checkin = await _clientPortalService.PerformOnlineCheckinAsync(
                            target.ClientId,
                            passenger.PassengerReservationId,
                            target.FlightId,
                            desiredSeatCode: null,
                            allowRandomSeat: true,
                            utcNow: DateTime.UtcNow,
                            cancellationToken);

                        completedTable.AddRow(
                            Markup.Escape(checkin.PassengerFullName),
                            Markup.Escape(checkin.SeatCode),
                            Markup.Escape(checkin.Gate),
                            checkin.BoardingAt.ToString("yyyy-MM-dd HH:mm"),
                            Markup.Escape(checkin.BoardingPassNumber));
                    }

                    AnsiConsole.Clear();
                    AnsiConsole.Write(new Rule("[bold deepskyblue1] Check-in completado [/]").LeftJustified());
                    AnsiConsole.Write(completedTable);
                    Pause();
                    continue;
                }

                var passengerLabel = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("\nSelecciona el pasajero:")
                        .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                        .AddChoices(candidates.PendingPassengers.Select(FormatPassengerChoice).Append("Volver")));

                if (passengerLabel == "Volver")
                    continue;

                var passengerReservationId = int.Parse(passengerLabel.Split('|')[0]);
                var (desiredSeat, allowRandomSeat) = await PromptSeatSelectionAsync(target.FlightId, cancellationToken);
                if (desiredSeat is null && !allowRandomSeat)
                    continue;

                if (!AnsiConsole.Confirm("¿Confirmar el check-in del pasajero?", true))
                    continue;

                var result = await _clientPortalService.PerformOnlineCheckinAsync(
                    target.ClientId,
                    passengerReservationId,
                    target.FlightId,
                    desiredSeat,
                    allowRandomSeat,
                    DateTime.UtcNow,
                    cancellationToken);

                if (result.AdditionalSeatChoiceCharge > 0m)
                {
                    AnsiConsole.MarkupLine(
                        $"\n[yellow]Cargo por elección de asiento:[/] [white]{result.AdditionalSeatChoiceCharge:0.00}[/]");
                }

                RenderBoardingPass(
                    result.PassengerFullName,
                    result.FlightCode,
                    $"{result.OriginIata} → {result.DestinationIata}",
                    result.Gate,
                    result.DepartureAt,
                    result.ArrivalAt,
                    result.BoardingAt,
                    result.SeatCode,
                    result.CabinTypeName,
                    result.BoardingPassNumber,
                    result.BoardingPassStatus);
                Pause();
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
            Pause();
        }
    }

    private async Task QueryBoardingPassAsync(CancellationToken cancellationToken)
    {
        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Buscar pase por:")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Código del tiquete", "Código del pase", "Documento del cliente", ConsoleMenuHelpers.VolverAlMenu));

        if (mode == ConsoleMenuHelpers.VolverAlMenu)
            return;

        var value = ConsoleMenuHelpers.PromptRequiredStringOrBack(mode + ":");
        if (value is null)
            return;

        var result = mode switch
        {
            "Código del tiquete" => await _boardingPassesService.GetByTicketCodeAsync(value, cancellationToken),
            "Código del pase" => await _boardingPassesService.GetByBoardingPassCodeAsync(value, cancellationToken),
            _ => await _boardingPassesService.GetByDocumentAsync(value, cancellationToken)
        };

        if (result is null)
        {
            AnsiConsole.MarkupLine("\n[yellow]No se encontró el pase de abordar.[/]");
            Pause();
            return;
        }

        RenderBoardingPass(
            result.PassengerName,
            result.FlightCode,
            result.RouteLabel,
            result.Gate.Value,
            result.DepartureAt,
            result.DepartureAt,
            result.BoardingAt.Value,
            result.SeatCode.Value,
            "Asignada",
            result.Code.Value,
            result.Status.Value);
        Pause();
    }

    private async Task QueryReadyPassengersAsync(CancellationToken cancellationToken)
    {
        var flights = (await _boardingPassesService.GetFlightsWithReadyPassengersAsync(cancellationToken)).ToList();
        if (flights.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No hay pasajeros listos para abordar.[/]");
            Pause();
            return;
        }

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Seleccione el vuelo:")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(flights.Select(x => $"{x.FlightId}|{x.FlightCode}|{x.RouteLabel}|{x.DepartureAt:yyyy-MM-dd HH:mm}").Append("Volver")));

        if (selected == "Volver")
            return;

        var flightId = int.Parse(selected.Split('|')[0]);
        var passengers = await _boardingPassesService.GetReadyToBoardByFlightAsync(flightId, cancellationToken);

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]Nombre[/]")
            .AddColumn("[bold grey]Documento[/]")
            .AddColumn("[bold grey]Asiento[/]")
            .AddColumn("[bold grey]Código tiquete[/]")
            .AddColumn("[bold grey]Estado[/]")
            .AddColumn("[bold grey]Check-in[/]");

        foreach (var passenger in passengers)
        {
            table.AddRow(
                Markup.Escape(passenger.PassengerName),
                Markup.Escape(passenger.PassengerDocument),
                Markup.Escape(passenger.SeatCode),
                Markup.Escape(passenger.TicketCode),
                Markup.Escape(passenger.TicketState),
                passenger.CheckedInAt.ToString("yyyy-MM-dd HH:mm"));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        Pause();
    }

    private async Task<(string? DesiredSeat, bool AllowRandomSeat)> PromptSeatSelectionAsync(int flightId, CancellationToken cancellationToken)
    {
        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nAsignación de asiento:")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Asignar automáticamente", "Elegir asiento", "Cancelar"));

        if (mode == "Cancelar")
            return (null, false);

        if (mode == "Asignar automáticamente")
            return (null, true);

        var availableSeats = (await _clientPortalService.GetAvailableSeatCodesAsync(flightId, cancellationToken)).ToList();
        if (availableSeats.Count == 0)
            throw new InvalidOperationException("No hay asientos disponibles.");

        var seat = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Seleccione el asiento:")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .PageSize(15)
                .AddChoices(availableSeats.Append("Cancelar")));

        return seat == "Cancelar" ? (null, false) : (seat, false);
    }

    private async Task<CheckinTarget?> ResolveTargetAsync(CancellationToken cancellationToken)
    {
        var mode = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Buscar para check-in por:")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Código del tiquete", "Código de reserva", "Volver"));

        if (mode == "Volver")
            return null;

        if (mode == "Código del tiquete")
        {
            var ticketCode = ConsoleMenuHelpers.PromptRequiredStringOrBack("Código del tiquete:");
            if (ticketCode is null)
                return null;

            var target = await (
                from ticket in _db.Tickets.AsNoTracking()
                join pr in _db.PassengerReservations.AsNoTracking() on ticket.PassengerReservation_Id equals pr.Id
                join fr in _db.FlightReservations.AsNoTracking() on pr.Flight_Reservation_Id equals fr.Id
                join bf in _db.BookingFlights.AsNoTracking() on fr.BookingFlightId equals bf.Id
                join bookingRow in _db.Bookings.AsNoTracking() on bf.BookingId equals bookingRow.Id
                join flight in _db.Flights.AsNoTracking() on bf.FlightId equals flight.Id
                where ticket.Code == ticketCode.Trim()
                select new CheckinTarget(bookingRow.ClientId, bookingRow.Id, flight.Id, ReservationReferenceCodec.Encode(bookingRow.Id), flight.FlightCode))
                .FirstOrDefaultAsync(cancellationToken);

            if (target is null)
                throw new InvalidOperationException("No se encontró el tiquete indicado.");

            return target;
        }

        var bookingLookup = ConsoleMenuHelpers.PromptRequiredStringOrBack("Código de reserva:");
        if (bookingLookup is null)
            return null;

        var bookingId = ResolveBookingId(bookingLookup);
        var booking = await _db.Bookings.AsNoTracking().FirstOrDefaultAsync(b => b.Id == bookingId, cancellationToken)
            ?? throw new InvalidOperationException("No se encontró la reserva indicada.");

        var flights = await (
            from bf in _db.BookingFlights.AsNoTracking()
            join flight in _db.Flights.AsNoTracking() on bf.FlightId equals flight.Id
            join route in _db.Routes.AsNoTracking() on flight.RouteId equals route.Id
            join ao in _db.Airports.AsNoTracking() on route.OriginAirportId equals ao.Id
            join ad in _db.Airports.AsNoTracking() on route.DestinationAirportId equals ad.Id
            where bf.BookingId == booking.Id
            orderby flight.DepartureAt
            select new
            {
                flight.Id,
                flight.FlightCode,
                RouteLabel = ao.IataCode + " → " + ad.IataCode,
                flight.DepartureAt
            })
            .ToListAsync(cancellationToken);

        if (flights.Count == 0)
            throw new InvalidOperationException("La reserva no tiene vuelos asociados.");

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Seleccione el vuelo:")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(flights.Select(f => $"{f.Id}|{f.FlightCode}|{f.RouteLabel}|{f.DepartureAt:yyyy-MM-dd HH:mm}").Append("Volver")));

        if (selected == "Volver")
            return null;

        var flightId = int.Parse(selected.Split('|')[0]);
        var flightCode = flights.First(f => f.Id == flightId).FlightCode;
        return new CheckinTarget(booking.ClientId, booking.Id, flightId, ReservationReferenceCodec.Encode(booking.Id), flightCode);
    }

    private static int ResolveBookingId(string raw)
    {
        if (ReservationReferenceCodec.TryParseReservationCode(raw, out var bookingId))
            return bookingId;

        if (int.TryParse(raw, out bookingId))
            return bookingId;

        throw new InvalidOperationException("Código de reserva inválido.");
    }

    private static void RenderPassengersTable(string title, IReadOnlyList<CheckinPassengerRow> passengers, bool includeBoardingPass)
    {
        AnsiConsole.Write(new Rule($"[grey]{Markup.Escape(title)}[/]").LeftJustified());

        if (passengers.Count == 0)
        {
            AnsiConsole.MarkupLine("[grey]No hay registros en esta sección.[/]\n");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]Pasajero[/]")
            .AddColumn("[bold grey]Documento[/]")
            .AddColumn("[bold grey]Tiquete[/]")
            .AddColumn("[bold grey]Estado[/]")
            .AddColumn("[bold grey]Asiento[/]");

        if (includeBoardingPass)
            table.AddColumn("[bold grey]Pase de abordar[/]");

        foreach (var passenger in passengers)
        {
            var row = new List<string>
            {
                Markup.Escape(passenger.PassengerName),
                Markup.Escape(passenger.PassengerDocument),
                Markup.Escape(passenger.TicketCode),
                includeBoardingPass ? "[green]Check-in realizado[/]" : Markup.Escape(passenger.TicketState),
                string.IsNullOrWhiteSpace(passenger.SeatCode) ? "[grey]—[/]" : Markup.Escape(passenger.SeatCode)
            };

            if (includeBoardingPass)
                row.Add(string.IsNullOrWhiteSpace(passenger.BoardingPassNumber) ? "[grey]—[/]" : Markup.Escape(passenger.BoardingPassNumber));

            table.AddRow(row.ToArray());
        }

        AnsiConsole.Write(table);
        AnsiConsole.WriteLine();
    }

    private static void RenderBoardingPass(
        string passenger,
        string flightCode,
        string route,
        string gate,
        DateTime departureAt,
        DateTime arrivalAt,
        DateTime boardingAt,
        string seatCode,
        string cabinType,
        string boardingPass,
        string boardingPassStatus)
    {
        var content =
$@"══════════════════════════════════════
       PASE DE ABORDAR
══════════════════════════════════════
Pasajero: {passenger}
Vuelo:    {flightCode}
Ruta:     {route}
Fecha:    {departureAt:yyyy-MM-dd}
Salida:   {departureAt:HH:mm}
Llegada:  {arrivalAt:HH:mm}
Asiento:  {seatCode}
Clase:    {cabinType}
Puerta:   {gate}
Abordaje: {boardingAt:HH:mm}
Estado:   {boardingPassStatus}
Código:   {boardingPass}
══════════════════════════════════════";

        AnsiConsole.Write(new Panel(new Markup(Markup.Escape(content))).Border(BoxBorder.Rounded).BorderColor(Color.DeepSkyBlue1));
    }

    private static string FormatPassengerChoice(CheckinPassengerRow passenger)
        => $"{passenger.PassengerReservationId}|{passenger.PassengerName}|{passenger.PassengerDocument}|{passenger.TicketCode}";

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Presiona Enter para continuar...[/]").AllowEmpty());
    }

    private sealed record CheckinTarget(int ClientId, int BookingId, int FlightId, string ReservationCode, string FlightCode);
}
