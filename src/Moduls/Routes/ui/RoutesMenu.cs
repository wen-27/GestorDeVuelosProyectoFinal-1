using GestorDeVuelosProyectoFinal.Moduls.Routes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.Routes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.Routes.ui;

public sealed class RoutesMenu : IModuleUI
{
    private readonly IRoutesService _service;

    public string Key => "routes";
    public string Title => "Gestion de Rutas";

    public RoutesMenu(IRoutesService service)
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
                    .Title("Seleccione una opcion:")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "1. Listar todas las rutas",
                        "2. Buscar por ID",
                        "3. Buscar por aeropuerto origen",
                        "4. Buscar por aeropuerto destino",
                        "5. Buscar por origen y destino",
                        "6. Registrar ruta",
                        "7. Actualizar ruta",
                        "8. Eliminar ruta por ID",
                        "0. Volver al menu principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option.Split('.')[0])
            {
                case "1": await ListAllAsync(); break;
                case "2": await SearchByIdAsync(); break;
                case "3": await SearchByOriginAirportIdAsync(); break;
                case "4": await SearchByDestinationAirportIdAsync(); break;
                case "5": await SearchByOriginAndDestinationAsync(); break;
                case "6": await CreateAsync(); break;
                case "7": await UpdateAsync(); break;
                case "8": await DeleteByIdAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todas las rutas");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la ruta:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            AnsiConsole.MarkupLine("[red]No se encontro ninguna ruta con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByOriginAirportIdAsync()
    {
        var airportId = AnsiConsole.Ask<int>("Ingrese el [green]ID del aeropuerto origen[/]:");
        var items = await _service.GetByOriginAirportIdAsync(airportId);
        ShowSearchResults(items, $"Rutas con origen #{airportId}");
    }

    private async Task SearchByDestinationAirportIdAsync()
    {
        var airportId = AnsiConsole.Ask<int>("Ingrese el [green]ID del aeropuerto destino[/]:");
        var items = await _service.GetByDestinationAirportIdAsync(airportId);
        ShowSearchResults(items, $"Rutas con destino #{airportId}");
    }

    private async Task SearchByOriginAndDestinationAsync()
    {
        var originAirportId = AnsiConsole.Ask<int>("Ingrese el [green]ID del aeropuerto origen[/]:");
        var destinationAirportId = AnsiConsole.Ask<int>("Ingrese el [green]ID del aeropuerto destino[/]:");
        var item = await _service.GetByOriginAndDestinationAsync(originAirportId, destinationAirportId);

        if (item is null)
            AnsiConsole.MarkupLine("[red]No se encontro ninguna ruta para esa combinacion.[/]");
        else
            ShowTable(new[] { item }, $"Ruta {originAirportId} -> {destinationAirportId}");

        Pause();
    }

    private async Task CreateAsync()
    {
        try
        {
            AnsiConsole.MarkupLine("[bold blue]Registrar ruta[/]");
            var originAirportId = AnsiConsole.Ask<int>("Airport ID origen:");
            var destinationAirportId = AnsiConsole.Ask<int>("Airport ID destino:");
            var distanceKm = AskOptionalInt("Distancia en KM (opcional):");
            var estimatedDurationMin = AskOptionalInt("Duracion estimada en minutos (opcional):");

            if (AnsiConsole.Confirm("Desea guardar los cambios?"))
            {
                await _service.CreateAsync(originAirportId, destinationAirportId, distanceKm, estimatedDurationMin);
                AnsiConsole.MarkupLine("[green]Ruta registrada exitosamente.[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] de la ruta a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]Ruta no encontrada.[/]");
            Pause();
            return;
        }

        try
        {
            var originAirportId = AnsiConsole.Ask<int>("Nuevo Airport ID origen:", item.OriginAirportId.Value);
            var destinationAirportId = AnsiConsole.Ask<int>("Nuevo Airport ID destino:", item.DestinationAirportId.Value);
            var distanceKm = AskOptionalInt("Nueva distancia en KM (opcional):", item.Distance.Value);
            var estimatedDurationMin = AskOptionalInt("Nueva duracion estimada en minutos (opcional):", item.Duration.Value);

            await _service.UpdateAsync(id, originAirportId, destinationAirportId, distanceKm, estimatedDurationMin);
            AnsiConsole.MarkupLine("[green]Ruta actualizada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeleteByIdAsync()
    {
        try
        {
            var id = AnsiConsole.Ask<int>("ID de la ruta a eliminar:");
            if (AnsiConsole.Confirm("Esta seguro?"))
            {
                await _service.DeleteByIdAsync(id);
                AnsiConsole.MarkupLine("[green]Ruta eliminada.[/]");
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowSearchResults(IEnumerable<Route> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<Route> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Origen[/]")
            .AddColumn("[blue]Destino[/]")
            .AddColumn("[green]Distancia KM[/]")
            .AddColumn("[blue]Duracion Min[/]");

        foreach (var item in items)
        {
            table.AddRow(
                (item.Id?.Value ?? 0).ToString(),
                item.OriginAirportId.Value.ToString(),
                item.DestinationAirportId.Value.ToString(),
                item.Distance.Value?.ToString() ?? "-",
                item.Duration.Value?.ToString() ?? "-");
        }

        AnsiConsole.Write(table);
    }

    private static int? AskOptionalInt(string prompt, int? currentValue = null)
    {
        var defaultValue = currentValue?.ToString() ?? string.Empty;
        var text = AnsiConsole.Ask<string>(prompt, defaultValue).Trim();

        if (string.IsNullOrWhiteSpace(text))
            return null;

        if (!int.TryParse(text, out var value))
            throw new InvalidOperationException("Debe ingresar un numero entero valido o dejar el valor vacio.");

        return value;
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
