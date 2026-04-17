using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.Cities.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Cities.ui;

public sealed class CitiesMenu : IModuleUI
{
    private readonly ICityService _service;
    public string Key => "cities";
    public string Title => "✈️  Gestión de Ciudades";

    public CitiesMenu(ICityService service)
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
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Listar todas las ciudades",
                        "2. Buscar ciudad por Nombre",
                        "3. Listar ciudades por Región (ID)",
                        "4. Registrar nueva Ciudad",
                        "5. Actualizar Ciudad",
                        "6. Eliminar Ciudad",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0")) break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByNameAsync(); break;
                case '3': await ListByRegionAsync(); break;
                case '4': await CreateAsync(); break;
                case '5': await UpdateAsync(); break;
                case '6': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var cities = await _service.GetAllAsync();
        ShowTable(cities, "Todas las Ciudades");
        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] de la ciudad:");
        var city = await _service.GetByNameAsync(name);

        if (city == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ninguna ciudad con ese nombre.[/]");
        else
            ShowTable(new[] { city }, $"Resultado para: {name}");

        Pause();
    }

    private async Task ListByRegionAsync()
    {
        var regionId = AnsiConsole.Ask<int>("Ingrese el [green]ID de la Región[/]:");
        var cities = await _service.GetByRegionIdAsync(regionId);
        ShowTable(cities, $"Ciudades en la Región #{regionId}");
        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nueva Ciudad[/]");
        var name = AnsiConsole.Ask<string>("Nombre de la ciudad:");
        var regionId = AnsiConsole.Ask<int>("ID de la región perteneciente:");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            await _service.CreateAsync(name, regionId);
            AnsiConsole.MarkupLine("[green]✅ Ciudad registrada exitosamente.[/]");
        }
        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] de la ciudad a modificar:");
        var city = await _service.GetByIdAsync(id);

        if (city == null)
        {
            AnsiConsole.MarkupLine("[red]❌ Ciudad no encontrada.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"Modificando: [bold]{city.Name.Value}[/]");
        var newName = AnsiConsole.Ask<string>("Nuevo nombre (deje igual para no cambiar):", city.Name.Value);
        var newRegionId = AnsiConsole.Ask<int>("Nuevo ID de Región:", city.RegionId.Value);

        await _service.UpdateAsync(id, newName, newRegionId);
        AnsiConsole.MarkupLine("[green]✅ Ciudad actualizada.[/]");
        Pause();
    }

    private async Task DeleteMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Menú de Eliminación[/]")
                .AddChoices("Eliminar por ID", "Eliminar por Nombre", "Eliminar todas por Región", "Cancelar"));

        switch (subOption)
        {
            case "Eliminar por ID":
                var id = AnsiConsole.Ask<int>("ID a eliminar:");
                if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                    await _service.DeleteAsync(id);
                break;
            case "Eliminar por Nombre":
                var name = AnsiConsole.Ask<string>("Nombre a eliminar:");
                await _service.DeleteByNameAsync(name);
                break;
            case "Eliminar todas por Región":
                var rId = AnsiConsole.Ask<int>("ID de la región:");
                await _service.DeleteByRegionIdAsync(rId);
                break;
        }
        AnsiConsole.MarkupLine("[green]✅ Operación procesada.[/]");
        Pause();
    }

    private void ShowTable(IEnumerable<GestorDeVuelosProyectoFinal.Moduls.Cities.Domain.Aggregate.City> cities, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]")
            .AddColumn("[blue]Región ID[/]");

        foreach (var c in cities)
            table.AddRow(c.Id.Value.ToString(), c.Name.Value, c.RegionId.Value.ToString());

        AnsiConsole.Write(table);
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}