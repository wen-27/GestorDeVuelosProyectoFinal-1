using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.DocumentTypes.ui;

public sealed class DocumentTypesMenu : IModuleUI
{
    private readonly IDocumentTypesService _service;

    public string Key => "document_types";
    public string Title => "📄  Gestión de Tipos de Documento";

    public DocumentTypesMenu(IDocumentTypesService service)
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
                        "1. Listar todos los tipos de documento",
                        "2. Buscar tipo por ID",
                        "3. Buscar tipo por Nombre",
                        "4. Buscar tipo por Código",
                        "5. Registrar nuevo Tipo",
                        "6. Actualizar Tipo",
                        "7. Eliminar Tipo",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByIdAsync(); break;
                case '3': await SearchByNameAsync(); break;
                case '4': await SearchByCodeAsync(); break;
                case '5': await CreateAsync(); break;
                case '6': await UpdateAsync(); break;
                case '7': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todos los Tipos de Documento");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del tipo de documento:");
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún tipo de documento con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] del tipo de documento:");
        var item = await _service.GetByNameAsync(name);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún tipo de documento con ese nombre.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para: {name}");

        Pause();
    }

    private async Task SearchByCodeAsync()
    {
        var code = AnsiConsole.Ask<string>("Ingrese el [green]código[/] del tipo de documento:");
        var item = await _service.GetByCodeAsync(code);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún tipo de documento con ese código.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para código: {code}");

        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nuevo Tipo de Documento[/]");
        var name = AnsiConsole.Ask<string>("Nombre del tipo de documento:");
        var code = AnsiConsole.Ask<string>("Código del documento:");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(name, code);
                AnsiConsole.MarkupLine("[green]✅ Tipo de documento registrado exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] del tipo de documento a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Tipo de documento no encontrado.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"Modificando: [bold]{item.Name.Value}[/] ([blue]{item.Code.Value}[/])");
        var newName = AnsiConsole.Ask<string>("Nuevo nombre (deje igual para no cambiar):", item.Name.Value);
        var newCode = AnsiConsole.Ask<string>("Nuevo código (deje igual para no cambiar):", item.Code.Value);

        try
        {
            await _service.UpdateAsync(id, newName, newCode);
            AnsiConsole.MarkupLine("[green]✅ Tipo de documento actualizado.[/]");
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
                .AddChoices("Eliminar por ID", "Eliminar por Nombre", "Eliminar por Código", "Cancelar"));

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
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        await _service.DeleteByIdAsync(id);
                    break;
                case "Eliminar por Nombre":
                    var name = AnsiConsole.Ask<string>("Nombre a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        await _service.DeleteByNameAsync(name);
                    break;
                case "Eliminar por Código":
                    var code = AnsiConsole.Ask<string>("Código a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        await _service.DeleteByCodeAsync(code);
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

    private static void ShowTable(IEnumerable<DocumentType> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]")
            .AddColumn("[blue]Código[/]");

        foreach (var item in items)
        {
            table.AddRow(item.Id.Value.ToString(), item.Name.Value, item.Code.Value);
        }

        AnsiConsole.Write(table);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
