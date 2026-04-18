using GestorDeVuelosProyectoFinal.Moduls.Seasons.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.Seasons.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.Seasons.ui;

public sealed class SeasonsMenu : IModuleUI
{
    private readonly ISeasonsService _service;

    public string Key => "seasons";
    public string Title => "Gestion de Temporadas";

    public SeasonsMenu(ISeasonsService service)
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
                        "1. Listar todas las temporadas",
                        "2. Buscar por ID",
                        "3. Buscar por nombre",
                        "4. Registrar temporada",
                        "5. Actualizar temporada",
                        "6. Eliminar temporada",
                        "0. Volver al menu principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option.Split('.')[0])
            {
                case "1": await ListAllAsync(); break;
                case "2": await SearchByIdAsync(); break;
                case "3": await SearchByNameAsync(); break;
                case "4": await CreateAsync(); break;
                case "5": await UpdateAsync(); break;
                case "6": await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todas las temporadas");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la temporada:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            AnsiConsole.MarkupLine("[red]No se encontro ninguna temporada con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByNameAsync()
    {
        var name = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] de la temporada:");
        var item = await _service.GetByNameAsync(name);

        if (item is null)
            AnsiConsole.MarkupLine("[red]No se encontro ninguna temporada con ese nombre.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para: {name}");

        Pause();
    }

    private async Task CreateAsync()
    {
        try
        {
            AnsiConsole.MarkupLine("[bold blue]Registrar temporada[/]");
            var name = AnsiConsole.Ask<string>("Nombre:");
            var description = AskOptionalText("Descripcion (opcional):");
            var priceFactor = AnsiConsole.Ask<decimal>("Price factor:");

            if (AnsiConsole.Confirm("Desea guardar los cambios?"))
            {
                await _service.CreateAsync(name, description, priceFactor);
                AnsiConsole.MarkupLine("[green]Temporada registrada exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] de la temporada a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]Temporada no encontrada.[/]");
            Pause();
            return;
        }

        try
        {
            var name = AnsiConsole.Ask<string>("Nuevo nombre:", item.Name.Value);
            var description = AskOptionalText("Nueva descripcion (opcional):", item.Description.Value);
            var priceFactor = AnsiConsole.Ask<decimal>("Nuevo price factor:", item.PriceFactor.Value);

            await _service.UpdateAsync(id, name, description, priceFactor);
            AnsiConsole.MarkupLine("[green]Temporada actualizada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeleteMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Menu de eliminacion[/]")
                .AddChoices("Eliminar por ID", "Eliminar por nombre", "Cancelar"));

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
                    if (AnsiConsole.Confirm("Esta seguro?"))
                        await _service.DeleteByIdAsync(id);
                    break;
                case "Eliminar por nombre":
                    var name = AnsiConsole.Ask<string>("Nombre a eliminar:");
                    if (AnsiConsole.Confirm("Esta seguro?"))
                        await _service.DeleteByNameAsync(name);
                    break;
            }

            AnsiConsole.MarkupLine("[green]Operacion procesada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowTable(IEnumerable<Season> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Nombre[/]")
            .AddColumn("[blue]Descripcion[/]")
            .AddColumn("[green]Price Factor[/]");

        foreach (var item in items)
        {
            table.AddRow(
                (item.Id?.Value ?? 0).ToString(),
                item.Name.Value,
                item.Description.Value ?? "-",
                item.PriceFactor.Value.ToString("0.0000"));
        }

        AnsiConsole.Write(table);
    }

    private static string? AskOptionalText(string prompt, string? currentValue = null)
    {
        var text = AnsiConsole.Ask<string>(prompt, currentValue ?? string.Empty);
        return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
