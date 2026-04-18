using Spectre.Console;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using AircraftManufacturerAggregate = GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.Domain.Aggregate.AircraftManufacturers;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftManufacturers.UI;

public sealed class AircraftManufacturersMenu : IModuleUI
{
    private readonly IAircraftManufacturersService _service;

    public AircraftManufacturersMenu(IAircraftManufacturersService service)
    {
        _service = service;
    }

    public string Key => "aircraft-manufacturers";
    public string Title => "Fabricantes de Aeronaves";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold yellow]Fabricantes de Aeronaves[/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione una opción:")
                    .PageSize(10)
                    .AddChoices(
                        "1. Listar todos",
                        "2. Buscar por ID",
                        "3. Buscar por nombre",
                        "4. Buscar por país",
                        "5. Crear fabricante",
                        "6. Actualizar fabricante",
                        "7. Eliminar por ID",
                        "8. Eliminar por nombre",
                        "9. Eliminar por país",
                        "0. Volver"));

            if (option.StartsWith("0"))
                return;

            try
            {
                switch (option.Split('.')[0])
                {
                    case "1": await ListAllAsync(cancellationToken); break;
                    case "2": await SearchByIdAsync(cancellationToken); break;
                    case "3": await SearchByNameAsync(cancellationToken); break;
                    case "4": await SearchByCountryAsync(cancellationToken); break;
                    case "5": await CreateAsync(cancellationToken); break;
                    case "6": await UpdateAsync(cancellationToken); break;
                    case "7": await DeleteByIdAsync(cancellationToken); break;
                    case "8": await DeleteByNameAsync(cancellationToken); break;
                    case "9": await DeleteByCountryAsync(cancellationToken); break;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
                Pause();
            }
        }
    }

    private async Task ListAllAsync(CancellationToken cancellationToken)
    {
        var items = await _service.GetAllAsync(cancellationToken);
        ShowTable(items, "Todos los fabricantes");
        Pause();
    }

    private async Task SearchByIdAsync(CancellationToken cancellationToken)
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del fabricante:");
        var item = await _service.GetByIdAsync(id, cancellationToken);

        if (item is null)
            AnsiConsole.MarkupLine("[yellow]No se encontró ningún fabricante con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID {id}");

        Pause();
    }

    private async Task SearchByNameAsync(CancellationToken cancellationToken)
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del fabricante:");
        var item = await _service.GetByNameAsync(name, cancellationToken);

        if (item is null)
            AnsiConsole.MarkupLine("[yellow]No se encontró ningún fabricante con ese nombre.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para {name}");

        Pause();
    }

    private async Task SearchByCountryAsync(CancellationToken cancellationToken)
    {
        var country = AnsiConsole.Ask<string>("Ingrese el [green]país[/] del fabricante:");
        var items = await _service.GetByCountryAsync(country, cancellationToken);
        ShowTable(items, $"Fabricantes en {country}");
        Pause();
    }

    private async Task CreateAsync(CancellationToken cancellationToken)
    {
        var name = AnsiConsole.Ask<string>("Nombre del fabricante:");
        var country = AnsiConsole.Ask<string>("País del fabricante:");

        await _service.CreateAsync(name, country, cancellationToken);
        AnsiConsole.MarkupLine("[green]Fabricante creado correctamente.[/]");
        Pause();
    }

    private async Task UpdateAsync(CancellationToken cancellationToken)
    {
        var id = AnsiConsole.Ask<int>("ID del fabricante a actualizar:");
        var existing = await _service.GetByIdAsync(id, cancellationToken);

        if (existing is null)
        {
            AnsiConsole.MarkupLine("[yellow]No se encontró el fabricante.[/]");
            Pause();
            return;
        }

        var name = AnsiConsole.Ask<string>("Nuevo nombre:", existing.Name.Value);
        var country = AnsiConsole.Ask<string>("Nuevo país:", existing.Country.Value);

        await _service.UpdateAsync(id, name, country, cancellationToken);
        AnsiConsole.MarkupLine("[green]Fabricante actualizado correctamente.[/]");
        Pause();
    }

    private async Task DeleteByIdAsync(CancellationToken cancellationToken)
    {
        var id = AnsiConsole.Ask<int>("ID del fabricante a eliminar:");

        if (AnsiConsole.Confirm("¿Confirma la eliminación?", false))
        {
            await _service.DeleteByIdAsync(id, cancellationToken);
            AnsiConsole.MarkupLine("[green]Fabricante eliminado.[/]");
        }

        Pause();
    }

    private async Task DeleteByNameAsync(CancellationToken cancellationToken)
    {
        var name = AnsiConsole.Ask<string>("Nombre del fabricante a eliminar:");

        if (AnsiConsole.Confirm("¿Confirma la eliminación?", false))
        {
            await _service.DeleteByNameAsync(name, cancellationToken);
            AnsiConsole.MarkupLine("[green]Fabricante eliminado.[/]");
        }

        Pause();
    }

    private async Task DeleteByCountryAsync(CancellationToken cancellationToken)
    {
        var country = AnsiConsole.Ask<string>("País cuyos fabricantes desea eliminar:");

        if (AnsiConsole.Confirm("¿Confirma la eliminación masiva?", false))
        {
            var deleted = await _service.DeleteByCountryAsync(country, cancellationToken);
            AnsiConsole.MarkupLine($"[green]Se eliminaron {deleted} fabricantes.[/]");
        }

        Pause();
    }

    private static void ShowTable(IEnumerable<AircraftManufacturerAggregate> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No hay registros para mostrar.[/]");
            return;
        }

        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]")
            .AddColumn("[blue]País[/]");

        foreach (var item in list)
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.Name.Value,
                item.Country.Value);
        }

        AnsiConsole.Write(table);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\n[grey]Presione ENTER para continuar...[/]");
        Console.ReadLine();
    }
}
