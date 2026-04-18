using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.PersonalPositions.ui;

public sealed class PersonalPositionsMenu : IModuleUI
{
    private readonly IPersonalPositionsService _service;

    public string Key => "staff_positions";
    public string Title => "🧑‍✈️  Gestión de Cargos del Personal";

    public PersonalPositionsMenu(IPersonalPositionsService service)
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
                        "1. Listar todos los cargos",
                        "2. Buscar por ID",
                        "3. Buscar por Nombre",
                        "4. Registrar nuevo Cargo",
                        "5. Actualizar Cargo",
                        "6. Eliminar Cargo",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByIdAsync(); break;
                case '3': await SearchByNameAsync(); break;
                case '4': await CreateAsync(); break;
                case '5': await UpdateAsync(); break;
                case '6': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todos los Cargos");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del cargo:");
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún cargo con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del cargo:");
        var item = await _service.GetByNameAsync(name);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún cargo con ese nombre.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para: {name}");

        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nuevo Cargo[/]");
        var name = AnsiConsole.Ask<string>("Nombre del cargo:");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(name);
                AnsiConsole.MarkupLine("[green]✅ Cargo registrado exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] del cargo a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Cargo no encontrado.[/]");
            Pause();
            return;
        }

        var newName = AnsiConsole.Ask<string>("Nuevo nombre:", item.Name.Value);

        try
        {
            await _service.UpdateAsync(id, newName);
            AnsiConsole.MarkupLine("[green]✅ Cargo actualizado.[/]");
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

    private static void ShowTable(IEnumerable<PersonalPosition> items, string title)
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
