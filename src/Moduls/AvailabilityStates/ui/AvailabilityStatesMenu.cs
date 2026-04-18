using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.AvailabilityStates.ui;

public sealed class AvailabilityStatesMenu : IModuleUI
{
    private readonly IAvailabilityStatesService _service;

    public string Key => "availability_statuses";
    public string Title => "📅  Gestión de Estados de Disponibilidad";

    public AvailabilityStatesMenu(IAvailabilityStatesService service)
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
                    .AddChoices(new[]
                    {
                        "1. Listar todos los estados",
                        "2. Buscar por ID",
                        "3. Buscar por Nombre",
                        "4. Buscar por Staff ID",
                        "5. Registrar nuevo Estado",
                        "6. Actualizar Estado",
                        "7. Eliminar Estado",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByIdAsync(); break;
                case '3': await SearchByNameAsync(); break;
                case '4': await SearchByStaffIdAsync(); break;
                case '5': await CreateAsync(); break;
                case '6': await UpdateAsync(); break;
                case '7': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todos los Estados de Disponibilidad");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del estado:");
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún estado con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del estado:");
        var item = await _service.GetByNameAsync(name);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún estado con ese nombre.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para: {name}");

        Pause();
    }

    private async Task SearchByStaffIdAsync()
    {
        var staffId = AnsiConsole.Ask<int>("Ingrese el [green]Staff ID[/]:");
        var items = await _service.GetByStaffIdAsync(staffId);
        ShowSearchResults(items, $"Estados de disponibilidad para staff #{staffId}");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nuevo Estado de Disponibilidad[/]");
        var name = AnsiConsole.Ask<string>("Nombre del estado:");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(name);
                AnsiConsole.MarkupLine("[green]✅ Estado registrado exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] del estado a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Estado no encontrado.[/]");
            Pause();
            return;
        }

        var newName = AnsiConsole.Ask<string>("Nuevo nombre:", item.Name.Value);

        try
        {
            await _service.UpdateAsync(id, newName);
            AnsiConsole.MarkupLine("[green]✅ Estado actualizado.[/]");
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
                .AddChoices("Eliminar por ID", "Eliminar por Nombre", "Cancelar"));

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
            }

            AnsiConsole.MarkupLine("[green]✅ Operación procesada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowSearchResults(IEnumerable<AvailabilityState> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]❌ No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<AvailabilityState> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]");

        foreach (var item in items)
            table.AddRow((item.Id?.Value ?? 0).ToString(), item.Name.Value);

        AnsiConsole.Write(table);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
