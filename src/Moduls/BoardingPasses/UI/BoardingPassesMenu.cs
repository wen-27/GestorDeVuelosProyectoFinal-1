using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.src.Moduls.BoardingPasses.UI;

public sealed class BoardingPassesMenu : IModuleUI
{
    private readonly IBoardingPassesService _service;

    public BoardingPassesMenu(IBoardingPassesService service)
    {
        _service = service;
    }

    public string Key => "boarding_passes";
    public string Title => "Pases de abordar";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Pases de abordar [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione una opción:")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "1. Consultar pase de abordar",
                        "2. Consultar pasajeros listos para abordar",
                        ConsoleMenuHelpers.VolverAlMenu));

            switch (option)
            {
                case "1. Consultar pase de abordar":
                    await QueryBoardingPassAsync(cancellationToken);
                    break;
                case "2. Consultar pasajeros listos para abordar":
                    await QueryReadyPassengersAsync(cancellationToken);
                    break;
                default:
                    return;
            }
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

        var prompt = mode switch
        {
            "Código del tiquete" => "Código del tiquete:",
            "Código del pase" => "Código del pase:",
            _ => "Documento del cliente:"
        };

        var query = ConsoleMenuHelpers.PromptRequiredStringOrBack(prompt);
        if (query is null)
            return;

        BoardingPass? result = mode switch
        {
            "Código del tiquete" => await _service.GetByTicketCodeAsync(query, cancellationToken),
            "Código del pase" => await _service.GetByBoardingPassCodeAsync(query, cancellationToken),
            _ => await _service.GetByDocumentAsync(query, cancellationToken)
        };

        if (result is null)
        {
            AnsiConsole.MarkupLine("\n[yellow]No se encontró el pase de abordar.[/]");
            Pause();
            return;
        }

        RenderBoardingPass(result);
        Pause();
    }

    private async Task QueryReadyPassengersAsync(CancellationToken cancellationToken)
    {
        var flights = (await _service.GetFlightsWithReadyPassengersAsync(cancellationToken)).ToList();
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
                .AddChoices(flights.Select(x => $"{x.FlightId}|{x.FlightCode}|{x.RouteLabel}|{x.DepartureAt:yyyy-MM-dd HH:mm}").Append(ConsoleMenuHelpers.VolverAlMenu)));

        if (selected == ConsoleMenuHelpers.VolverAlMenu)
            return;

        var flightId = int.Parse(selected.Split('|')[0]);
        var passengers = await _service.GetReadyToBoardByFlightAsync(flightId, cancellationToken);

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

    private static void RenderBoardingPass(BoardingPass result)
    {
        var panel = new Panel(
            $"Pasajero: [bold]{Markup.Escape(result.PassengerName)}[/]\n" +
            $"Documento: [bold]{Markup.Escape(result.PassengerDocument)}[/]\n" +
            $"Código del pase: [bold]{Markup.Escape(result.Code.Value)}[/]\n" +
            $"Código del tiquete: [bold]{Markup.Escape(result.TicketCode)}[/]\n" +
            $"Vuelo: [bold]{Markup.Escape(result.FlightCode)}[/]\n" +
            $"Ruta: [bold]{Markup.Escape(result.RouteLabel)}[/]\n" +
            $"Asiento: [bold]{Markup.Escape(result.SeatCode.Value)}[/]\n" +
            $"Puerta: [bold]{Markup.Escape(result.Gate.Value)}[/]\n" +
            $"Hora de abordaje: [bold]{result.BoardingAt.Value:yyyy-MM-dd HH:mm}[/]\n" +
            $"Estado: [bold]{Markup.Escape(result.Status.Value)}[/]")
            .Header("[grey]Pase de abordar[/]")
            .BorderColor(Color.DeepSkyBlue1);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(panel);
    }

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Presiona Enter para continuar...[/]").AllowEmpty());
    }
}
