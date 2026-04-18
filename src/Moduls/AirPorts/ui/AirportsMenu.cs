using GestorDeVuelosProyectoFinal.Moduls.Airports.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.Airports.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.Airports.ui;

public sealed class AirportsMenu : IModuleUI
{
    private readonly IAirportsService _service;

    public string Key => "airports";
    public string Title => "🛬  Gestión de Aeropuertos";

    public AirportsMenu(IAirportsService service)
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
                    .PageSize(12)
                    .AddChoices(new[]
                    {
                        "1. Listar todos los aeropuertos",
                        "2. Buscar por ID",
                        "3. Buscar por Nombre",
                        "4. Buscar por IATA",
                        "5. Buscar por ICAO",
                        "6. Buscar por Ciudad ID",
                        "7. Buscar por Nombre de Ciudad",
                        "8. Registrar nuevo Aeropuerto",
                        "9. Actualizar Aeropuerto",
                        "10. Eliminar Aeropuerto",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option.Split('.')[0])
            {
                case "1": await ListAllAsync(); break;
                case "2": await SearchByIdAsync(); break;
                case "3": await SearchByNameAsync(); break;
                case "4": await SearchByIataAsync(); break;
                case "5": await SearchByIcaoAsync(); break;
                case "6": await SearchByCityIdAsync(); break;
                case "7": await SearchByCityNameAsync(); break;
                case "8": await CreateAsync(); break;
                case "9": await UpdateAsync(); break;
                case "10": await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todos los Aeropuertos");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del aeropuerto:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún aeropuerto con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del aeropuerto:");
        var item = await _service.GetByNameAsync(name);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún aeropuerto con ese nombre.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para: {name}");

        Pause();
    }

    private async Task SearchByIataAsync()
    {
        var iata = AnsiConsole.Ask<string>("Ingrese el [green]código IATA[/] del aeropuerto:");
        var item = await _service.GetByIataCodeAsync(iata);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún aeropuerto con ese código IATA.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para IATA: {iata.ToUpperInvariant()}");

        Pause();
    }

    private async Task SearchByIcaoAsync()
    {
        var icao = AnsiConsole.Ask<string>("Ingrese el [green]código ICAO[/] del aeropuerto:");
        var item = await _service.GetByIcaoCodeAsync(icao);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún aeropuerto con ese código ICAO.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ICAO: {icao.ToUpperInvariant()}");

        Pause();
    }

    private async Task SearchByCityIdAsync()
    {
        var cityId = AnsiConsole.Ask<int>("Ingrese el [green]ID de la ciudad[/]:");
        var items = await _service.GetByCityIdAsync(cityId);
        ShowSearchResults(items, $"Aeropuertos de la ciudad #{cityId}");
    }

    private async Task SearchByCityNameAsync()
    {
        var cityName = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] de la ciudad:");
        var items = await _service.GetByCityNameAsync(cityName);
        ShowSearchResults(items, $"Aeropuertos de la ciudad: {cityName}");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nuevo Aeropuerto[/]");
        var name = AnsiConsole.Ask<string>("Nombre:");
        var iataCode = AnsiConsole.Ask<string>("Código IATA (3 letras):");
        var icaoCode = AnsiConsole.Ask<string>("Código ICAO (4 letras, opcional):", string.Empty);
        var cityId = AnsiConsole.Ask<int>("ID de la ciudad:");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(name, iataCode, string.IsNullOrWhiteSpace(icaoCode) ? null : icaoCode, cityId);
                AnsiConsole.MarkupLine("[green]✅ Aeropuerto registrado exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] del aeropuerto a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Aeropuerto no encontrado.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"Modificando: [bold]{item.Name.Value}[/] ([blue]{item.IataCode.Value}[/])");
        var name = AnsiConsole.Ask<string>("Nuevo nombre:", item.Name.Value);
        var iataCode = AnsiConsole.Ask<string>("Nuevo código IATA:", item.IataCode.Value);
        var icaoCode = AnsiConsole.Ask<string>("Nuevo código ICAO:", item.IcaoCode.Value ?? string.Empty);
        var cityId = AnsiConsole.Ask<int>("Nuevo ID de la ciudad:", item.CityId.Value);

        try
        {
            await _service.UpdateAsync(id, name, iataCode, string.IsNullOrWhiteSpace(icaoCode) ? null : icaoCode, cityId);
            AnsiConsole.MarkupLine("[green]✅ Aeropuerto actualizado.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeleteMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Menú de Eliminación[/]")
                .AddChoices("Eliminar por ID", "Eliminar por Nombre", "Eliminar por IATA", "Eliminar por ICAO", "Cancelar"));

        if (subOption == "Cancelar")
        {
            Pause();
            return;
        }

        try
        {
            switch (subOption)
            {
                case "Eliminar por ID":
                    var id = AnsiConsole.Ask<int>("ID a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro?[/]"))
                        await _service.DeleteByIdAsync(id);
                    break;
                case "Eliminar por Nombre":
                    var name = AnsiConsole.Ask<string>("Nombre a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro?[/]"))
                        await _service.DeleteByNameAsync(name);
                    break;
                case "Eliminar por IATA":
                    var iata = AnsiConsole.Ask<string>("Código IATA a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro?[/]"))
                        await _service.DeleteByIataCodeAsync(iata);
                    break;
                case "Eliminar por ICAO":
                    var icao = AnsiConsole.Ask<string>("Código ICAO a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro?[/]"))
                        await _service.DeleteByIcaoCodeAsync(icao);
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

    private static void ShowSearchResults(IEnumerable<Airport> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]❌ No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<Airport> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]")
            .AddColumn("[blue]IATA[/]")
            .AddColumn("[blue]ICAO[/]")
            .AddColumn("[green]Ciudad ID[/]");

        foreach (var item in items)
        {
            table.AddRow(
                (item.Id?.Value ?? 0).ToString(),
                item.Name.Value,
                item.IataCode.Value,
                item.IcaoCode.Value ?? "-",
                item.CityId.Value.ToString());
        }

        AnsiConsole.Write(table);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
