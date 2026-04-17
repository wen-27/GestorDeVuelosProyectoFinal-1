using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.StreetTypes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.Moduls.StreetTypes.UI;

public sealed class StreetTypeMenu : IModuleUI
{
    private readonly IStreetTypeService _service;
    
    // Propiedades de IModuleUI para consistencia con Cities
    public string Key => "street_types";
    public string Title => "🛣️  Gestión de Tipos de Vía";

    public StreetTypeMenu(IStreetTypeService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            // Regla superior igual a la de Cities
            AnsiConsole.Write(new Rule($"[yellow]{Title}[/]").RuleStyle("grey").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione una opción:")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "1. Listar todos los tipos de vía",
                        "2. Buscar por Nombre",
                        "3. Registrar nuevo Tipo",
                        "4. Actualizar Tipo",
                        "5. Eliminar Tipo",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0")) break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByNameAsync(); break;
                case '3': await CreateAsync(); break;
                case '4': await UpdateAsync(); break;
                case '5': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var types = await _service.GetAllAsync();
        ShowTable(types, "Todos los Tipos de Vía");
        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del tipo de vía:");
        var type = await _service.GetByNameAsync(name);

        if (type == null)
        {
            AnsiConsole.MarkupLine("\n[red]❌ No se encontró ningún tipo de vía con ese nombre.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("\n[green][bold]✓ Registro Encontrado[/][/]");
            ShowTable(new[] { type }, $"Resultado para: {name}");
        }
        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("\n[bold blue]--- REGISTRAR NUEVO TIPO DE VÍA ---[/]");
        var name = AnsiConsole.Ask<string>("Nombre del tipo de vía:");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            await _service.CreateAsync(name);
            AnsiConsole.MarkupLine("\n[green]✅ Tipo de vía registrado exitosamente.[/]");
        }
        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = AnsiConsole.Ask<int>("\nIngrese el [yellow]ID[/] del tipo de vía a modificar:");
        var type = await _service.GetByIdAsync(id);

        if (type == null)
        {
            AnsiConsole.MarkupLine("[red]❌ Registro no encontrado.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"\nModificando: [bold cyan]{type.Name.Value}[/]");
        var newName = AnsiConsole.Ask<string>("Nuevo nombre:", type.Name.Value);

        await _service.UpdateAsync(id, newName);
        AnsiConsole.MarkupLine("\n[green]✅ Registro actualizado correctamente.[/]");
        Pause();
    }

    private async Task DeleteMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\n[red]Menú de Eliminación[/]")
                .AddChoices("Eliminar por ID", "Eliminar por Nombre", "Cancelar"));

        if (subOption == "Cancelar") return;

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
        }
        AnsiConsole.MarkupLine("\n[green]✅ Operación procesada con éxito.[/]");
        Pause();
    }

    private void ShowTable(IEnumerable<GestorDeVuelosProyectoFinal.Moduls.StreetTypes.Domain.Aggregate.StreetType> types, string title)
    {
        // Tabla automática con el mismo estilo que Cities
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]");

        foreach (var t in types)
        {
            table.AddRow(t.Id.Value.ToString(), t.Name.Value);
        }

        AnsiConsole.Write(table);
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}