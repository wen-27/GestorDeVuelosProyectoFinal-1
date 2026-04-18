using GestorDeVuelosProyectoFinal.Moduls.AirportAirline.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.AirportAirline.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.AirportAirline.ui;

public sealed class AirportAirlineMenu : IModuleUI
{
    private readonly IAirportAirlineService _service;

    public string Key => "airport_airline";
    public string Title => "🛫  Gestión de Operación Aeropuerto-Aerolínea";

    public AirportAirlineMenu(IAirportAirlineService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[yellow]{Title}[/]").RuleStyle("grey").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione una opción:")
                    .PageSize(14)
                    .AddChoices(new[]
                    {
                        "1. Listar todas las operaciones",
                        "2. Listar solo operaciones activas",
                        "3. Buscar por ID",
                        "4. Buscar por Terminal",
                        "5. Buscar por Airport ID",
                        "6. Buscar por Airline ID",
                        "7. Buscar por Start Date",
                        "8. Buscar por End Date",
                        "9. Registrar nueva Operación",
                        "10. Actualizar Operación",
                        "11. Desactivar Operación",
                        "12. Reactivar Operación",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option.Split('.')[0])
            {
                case "1": await ListAllAsync(); break;
                case "2": await ListActiveAsync(); break;
                case "3": await SearchByIdAsync(); break;
                case "4": await SearchByTerminalAsync(); break;
                case "5": await SearchByAirportIdAsync(); break;
                case "6": await SearchByAirlineIdAsync(); break;
                case "7": await SearchByStartDateAsync(); break;
                case "8": await SearchByEndDateAsync(); break;
                case "9": await CreateAsync(); break;
                case "10": await UpdateAsync(); break;
                case "11": await DeactivateMenuAsync(); break;
                case "12": await ReactivateAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todas las Operaciones");
        Pause();
    }

    private async Task ListActiveAsync()
    {
        var items = await _service.GetActiveAsync();
        ShowTable(items, "Operaciones Activas");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la operación:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ninguna operación con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByTerminalAsync()
    {
        var terminal = AnsiConsole.Ask<string>("Ingrese la [green]terminal[/]:");
        var items = await _service.GetByTerminalAsync(terminal);
        ShowSearchResults(items, $"Operaciones con terminal: {terminal}");
    }

    private async Task SearchByAirportIdAsync()
    {
        var airportId = AnsiConsole.Ask<int>("Ingrese el [green]Airport ID[/]:");
        var items = await _service.GetByAirportIdAsync(airportId);
        ShowSearchResults(items, $"Operaciones para aeropuerto #{airportId}");
    }

    private async Task SearchByAirlineIdAsync()
    {
        var airlineId = AnsiConsole.Ask<int>("Ingrese el [green]Airline ID[/]:");
        var items = await _service.GetByAirlineIdAsync(airlineId);
        ShowSearchResults(items, $"Operaciones para aerolínea #{airlineId}");
    }

    private async Task SearchByStartDateAsync()
    {
        var startDate = AnsiConsole.Ask<DateTime>("Ingrese la [green]fecha de inicio[/] (yyyy-MM-dd):");
        var items = await _service.GetByStartDateAsync(startDate);
        ShowSearchResults(items, $"Operaciones con inicio: {startDate:yyyy-MM-dd}");
    }

    private async Task SearchByEndDateAsync()
    {
        var endDate = AnsiConsole.Ask<DateTime>("Ingrese la [green]fecha de fin[/] (yyyy-MM-dd):");
        var items = await _service.GetByEndDateAsync(endDate);
        ShowSearchResults(items, $"Operaciones con fin: {endDate:yyyy-MM-dd}");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nueva Operación[/]");
        var airportId = AnsiConsole.Ask<int>("Airport ID:");
        var airlineId = AnsiConsole.Ask<int>("Airline ID:");
        var terminal = AnsiConsole.Ask<string>("Terminal (opcional):", string.Empty);
        var startDate = AnsiConsole.Ask<DateTime>("Start Date (yyyy-MM-dd):");
        var endDateInput = AnsiConsole.Ask<string>("End Date (opcional, yyyy-MM-dd):", string.Empty);
        var isActive = AnsiConsole.Confirm("¿Registrar como activa?", true);

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(
                    airportId,
                    airlineId,
                    string.IsNullOrWhiteSpace(terminal) ? null : terminal,
                    startDate,
                    ParseNullableDate(endDateInput),
                    isActive);
                AnsiConsole.MarkupLine("[green]✅ Operación registrada exitosamente.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
            }
        }

        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] de la operación a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Operación no encontrada.[/]");
            Pause();
            return;
        }

        var airportId = AnsiConsole.Ask<int>("Nuevo Airport ID:", item.AirportId.Value);
        var airlineId = AnsiConsole.Ask<int>("Nuevo Airline ID:", item.AirlineId.Value);
        var terminal = AnsiConsole.Ask<string>("Nueva terminal:", item.Terminal.Value ?? string.Empty);
        var startDate = AnsiConsole.Ask<DateTime>("Nueva Start Date:", item.StartDate.Value);
        var endDateInput = AnsiConsole.Ask<string>("Nueva End Date:", item.EndDate.Value?.ToString("yyyy-MM-dd") ?? string.Empty);
        var isActive = AnsiConsole.Confirm("¿La operación está activa?", item.IsActive.Value);

        try
        {
            await _service.UpdateAsync(id, airportId, airlineId, string.IsNullOrWhiteSpace(terminal) ? null : terminal, startDate, ParseNullableDate(endDateInput), isActive);
            AnsiConsole.MarkupLine("[green]✅ Operación actualizada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeactivateMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Menú de Desactivación[/]")
                .AddChoices("Desactivar por ID", "Desactivar por Terminal", "Desactivar por Airport ID", "Desactivar por Airline ID", "Desactivar por Start Date", "Desactivar por End Date", "Cancelar"));

        if (subOption == "Cancelar")
        {
            Pause();
            return;
        }

        try
        {
            switch (subOption)
            {
                case "Desactivar por ID":
                    var id = AnsiConsole.Ask<int>("ID a desactivar:");
                    if (AnsiConsole.Confirm("¿Desea desactivar la operación?"))
                        await _service.DeactivateByIdAsync(id);
                    break;
                case "Desactivar por Terminal":
                    var terminal = AnsiConsole.Ask<string>("Terminal a desactivar:");
                    if (AnsiConsole.Confirm("¿Desea desactivar las operaciones?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByTerminalAsync(terminal)}[/]");
                    break;
                case "Desactivar por Airport ID":
                    var airportId = AnsiConsole.Ask<int>("Airport ID:");
                    if (AnsiConsole.Confirm("¿Desea desactivar las operaciones?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByAirportIdAsync(airportId)}[/]");
                    break;
                case "Desactivar por Airline ID":
                    var airlineId = AnsiConsole.Ask<int>("Airline ID:");
                    if (AnsiConsole.Confirm("¿Desea desactivar las operaciones?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByAirlineIdAsync(airlineId)}[/]");
                    break;
                case "Desactivar por Start Date":
                    var startDate = AnsiConsole.Ask<DateTime>("Start Date (yyyy-MM-dd):");
                    if (AnsiConsole.Confirm("¿Desea desactivar las operaciones?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByStartDateAsync(startDate)}[/]");
                    break;
                case "Desactivar por End Date":
                    var endDate = AnsiConsole.Ask<DateTime>("End Date (yyyy-MM-dd):");
                    if (AnsiConsole.Confirm("¿Desea desactivar las operaciones?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByEndDateAsync(endDate)}[/]");
                    break;
            }

            AnsiConsole.MarkupLine("[green]✅ Operación procesada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private async Task ReactivateAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la operación a reactivar:");

        try
        {
            await _service.ReactivateAsync(id);
            AnsiConsole.MarkupLine("[green]✅ Operación reactivada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowSearchResults(IEnumerable<AirportAirlineOperation> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]❌ No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<AirportAirlineOperation> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Airport ID[/]")
            .AddColumn("[blue]Airline ID[/]")
            .AddColumn("[green]Terminal[/]")
            .AddColumn("[blue]Start Date[/]")
            .AddColumn("[blue]End Date[/]")
            .AddColumn("[green]Activo[/]");

        foreach (var item in items)
            table.AddRow(
                (item.Id?.Value ?? 0).ToString(),
                item.AirportId.Value.ToString(),
                item.AirlineId.Value.ToString(),
                item.Terminal.Value ?? "-",
                item.StartDate.Value.ToString("yyyy-MM-dd"),
                item.EndDate.Value?.ToString("yyyy-MM-dd") ?? "-",
                item.IsActive.Value ? "Sí" : "No");

        AnsiConsole.Write(table);
    }

    private static DateTime? ParseNullableDate(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        return DateTime.Parse(input);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
