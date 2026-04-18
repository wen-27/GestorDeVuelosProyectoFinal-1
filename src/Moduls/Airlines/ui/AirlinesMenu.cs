using GestorDeVuelosProyectoFinal.Moduls.Airlines.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.Airlines.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.Airlines.ui;

public sealed class AirlinesMenu : IModuleUI
{
    private readonly IAirlinesService _service;

    public string Key => "airlines";
    public string Title => "✈️  Gestión de Aerolíneas";

    public AirlinesMenu(IAirlinesService service)
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
                        "1. Listar todas las aerolíneas",
                        "2. Listar solo aerolíneas activas",
                        "3. Buscar por ID",
                        "4. Buscar por Nombre",
                        "5. Buscar por IATA",
                        "6. Buscar por País de Origen (ID)",
                        "7. Registrar nueva Aerolínea",
                        "8. Actualizar Aerolínea",
                        "9. Desactivar Aerolínea",
                        "10. Reactivar Aerolínea",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option.Split('.')[0])
            {
                case "1": await ListAllAsync(); break;
                case "2": await ListActiveAsync(); break;
                case "3": await SearchByIdAsync(); break;
                case "4": await SearchByNameAsync(); break;
                case "5": await SearchByIataCodeAsync(); break;
                case "6": await SearchByOriginCountryIdAsync(); break;
                case "7": await CreateAsync(); break;
                case "8": await UpdateAsync(); break;
                case "9": await DeactivateMenuAsync(); break;
                case "10": await ReactivateAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todas las Aerolíneas");
        Pause();
    }

    private async Task ListActiveAsync()
    {
        var items = await _service.GetActiveAsync();
        ShowTable(items, "Aerolíneas Activas");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la aerolínea:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ninguna aerolínea con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] de la aerolínea:");
        var item = await _service.GetByNameAsync(name);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ninguna aerolínea con ese nombre.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para: {name}");

        Pause();
    }

    private async Task SearchByIataCodeAsync()
    {
        var code = AnsiConsole.Ask<string>("Ingrese el [green]código IATA[/] de la aerolínea:");
        var item = await _service.GetByIataCodeAsync(code);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ninguna aerolínea con ese código IATA.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para IATA: {code.ToUpperInvariant()}");

        Pause();
    }

    private async Task SearchByOriginCountryIdAsync()
    {
        var originCountryId = AnsiConsole.Ask<int>("Ingrese el [green]ID del país de origen[/]:");
        var items = await _service.GetByOriginCountryIdAsync(originCountryId);
        ShowSearchResults(items, $"Aerolíneas del país #{originCountryId}");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nueva Aerolínea[/]");
        var name = AnsiConsole.Ask<string>("Nombre:");
        var iataCode = AnsiConsole.Ask<string>("Código IATA (3 letras):");
        var originCountryId = AnsiConsole.Ask<int>("ID del país de origen:");
        var isActive = AnsiConsole.Confirm("¿Registrar como activa?", true);

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(name, iataCode, originCountryId, isActive);
                AnsiConsole.MarkupLine("[green]✅ Aerolínea registrada exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] de la aerolínea a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Aerolínea no encontrada.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"Modificando: [bold]{item.Name.Value}[/] ([blue]{item.IataCode.Value}[/])");
        var name = AnsiConsole.Ask<string>("Nuevo nombre:", item.Name.Value);
        var iataCode = AnsiConsole.Ask<string>("Nuevo código IATA:", item.IataCode.Value);
        var originCountryId = AnsiConsole.Ask<int>("Nuevo ID del país de origen:", item.OriginCountryId.Value);
        var isActive = AnsiConsole.Confirm("¿La aerolínea está activa?", item.IsActive.Value);

        try
        {
            await _service.UpdateAsync(id, name, iataCode, originCountryId, isActive);
            AnsiConsole.MarkupLine("[green]✅ Aerolínea actualizada.[/]");
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
                .AddChoices("Desactivar por ID", "Desactivar por Nombre", "Desactivar por IATA", "Desactivar todas por País", "Cancelar"));

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
                    if (AnsiConsole.Confirm("¿Desea desactivar la aerolínea?"))
                        await _service.DeactivateByIdAsync(id);
                    break;
                case "Desactivar por Nombre":
                    var name = AnsiConsole.Ask<string>("Nombre a desactivar:");
                    if (AnsiConsole.Confirm("¿Desea desactivar la aerolínea?"))
                        await _service.DeactivateByNameAsync(name);
                    break;
                case "Desactivar por IATA":
                    var iataCode = AnsiConsole.Ask<string>("Código IATA a desactivar:");
                    if (AnsiConsole.Confirm("¿Desea desactivar la aerolínea?"))
                        await _service.DeactivateByIataCodeAsync(iataCode);
                    break;
                case "Desactivar todas por País":
                    var originCountryId = AnsiConsole.Ask<int>("ID del país:");
                    if (AnsiConsole.Confirm("¿Desea desactivar todas las aerolíneas de ese país?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByOriginCountryIdAsync(originCountryId)}[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la aerolínea a reactivar:");

        try
        {
            await _service.ReactivateAsync(id);
            AnsiConsole.MarkupLine("[green]✅ Aerolínea reactivada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowSearchResults(IEnumerable<Airline> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]❌ No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<Airline> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]")
            .AddColumn("[blue]IATA[/]")
            .AddColumn("[green]País Origen ID[/]")
            .AddColumn("[blue]Activo[/]")
            .AddColumn("[grey]Creado[/]")
            .AddColumn("[grey]Actualizado[/]");

        foreach (var item in items)
        {
            table.AddRow(
                (item.Id?.Value ?? 0).ToString(),
                item.Name.Value,
                item.IataCode.Value,
                item.OriginCountryId.Value.ToString(),
                item.IsActive.Value ? "Sí" : "No",
                item.CreatedIn.Value.ToString("yyyy-MM-dd HH:mm"),
                item.UpdatedIn.Value.ToString("yyyy-MM-dd HH:mm"));
        }

        AnsiConsole.Write(table);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
