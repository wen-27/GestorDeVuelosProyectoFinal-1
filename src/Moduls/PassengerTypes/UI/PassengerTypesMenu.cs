using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.Moduls.PassengerTypes.UI;

public sealed class PassengerTypesMenu : IModuleUI
{
    private readonly IPassengerTypesService _service;

    public PassengerTypesMenu(IPassengerTypesService service)
    {
        _service = service;
    }

    public string Key => "passenger-types";
    public string Title => "Tipos de pasajero";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Tipos de pasajero [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Usa las flechas para navegar[/]")
                    .PageSize(12)
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "Listar todos",
                        "Buscar por ID",
                        "Buscar por nombre",
                        "Buscar por edad (años)",
                        "Resolver por fecha de nacimiento",
                        "Crear tipo",
                        "Actualizar tipo",
                        "Eliminar por ID",
                        "Eliminar por nombre",
                        "Eliminar por edad (rangos que la contengan)",
                        "Volver"));

            switch (option)
            {
                case "Listar todos":
                    await ListAllAsync(cancellationToken);
                    break;
                case "Buscar por ID":
                    await SearchByIdAsync(cancellationToken);
                    break;
                case "Buscar por nombre":
                    await SearchByNameAsync(cancellationToken);
                    break;
                case "Buscar por edad (años)":
                    await SearchByAgeAsync(cancellationToken);
                    break;
                case "Resolver por fecha de nacimiento":
                    await ResolveByBirthAsync(cancellationToken);
                    break;
                case "Crear tipo":
                    await CreateAsync(cancellationToken);
                    break;
                case "Actualizar tipo":
                    await UpdateAsync(cancellationToken);
                    break;
                case "Eliminar por ID":
                    await DeleteByIdAsync(cancellationToken);
                    break;
                case "Eliminar por nombre":
                    await DeleteByNameAsync(cancellationToken);
                    break;
                case "Eliminar por edad (rangos que la contengan)":
                    await DeleteByAgeAsync(cancellationToken);
                    break;
                case "Volver":
                    return;
            }
        }
    }

    private async Task ListAllAsync(CancellationToken cancellationToken)
    {
        RenderTable(await _service.GetAllAsync(cancellationToken), "Tipos de pasajero");
        Pause();
    }

    private async Task SearchByIdAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID:");
        var item = await _service.GetByIdAsync(id, cancellationToken);
        RenderTable(item is null ? Array.Empty<PassengerType>() : new[] { item }, $"ID {id}");
        Pause();
    }

    private async Task SearchByNameAsync(CancellationToken cancellationToken)
    {
        var name = PromptRequiredText("Nombre:");
        var item = await _service.GetByNameAsync(name, cancellationToken);
        RenderTable(item is null ? Array.Empty<PassengerType>() : new[] { item }, name);
        Pause();
    }

    private async Task SearchByAgeAsync(CancellationToken cancellationToken)
    {
        var age = PromptNonNegativeInt("Edad en años:");
        var item = await _service.GetByAgeAsync(age, cancellationToken);
        RenderTable(item is null ? Array.Empty<PassengerType>() : new[] { item }, $"Edad {age}");
        Pause();
    }

    private async Task ResolveByBirthAsync(CancellationToken cancellationToken)
    {
        var birthText = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Fecha de nacimiento (yyyy-MM-dd):[/]")
                .Validate(s => DateTime.TryParse(s, out _) ? ValidationResult.Success() : ValidationResult.Error("[red]Fecha invalida.[/]")));
        var birth = DateTime.Parse(birthText).Date;

        var refText = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Fecha de referencia (yyyy-MM-dd), vacío = hoy UTC:[/]")
                .AllowEmpty());
        DateTime? reference = string.IsNullOrWhiteSpace(refText) ? null : DateTime.Parse(refText).Date;

        try
        {
            var item = await _service.ResolveByBirthDateAsync(birth, reference, cancellationToken);
            RenderTable(item is null ? Array.Empty<PassengerType>() : new[] { item }, "Resolución por fecha de nacimiento");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task CreateAsync(CancellationToken cancellationToken)
    {
        var name = PromptRequiredText("Nombre:");
        var minAge = PromptOptionalInt("min_age (vacío = null):");
        var maxAge = PromptOptionalInt("max_age (vacío = null):");

        try
        {
            await _service.CreateAsync(name, minAge, maxAge, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Tipo creado correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task UpdateAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID a actualizar:");
        var current = await _service.GetByIdAsync(id, cancellationToken);
        if (current is null)
        {
            AnsiConsole.MarkupLine("\n[yellow]No se encontró el tipo.[/]");
            Pause();
            return;
        }

        var name = PromptRequiredText("Nombre:", current.Name.Value);
        var minAge = PromptOptionalInt("min_age (vacío = null):", current.MinAge.Value);
        var maxAge = PromptOptionalInt("max_age (vacío = null):", current.MaxAge.Value);

        try
        {
            await _service.UpdateAsync(id, name, minAge, maxAge, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Tipo actualizado correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task DeleteByIdAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID a eliminar:");
        if (!AnsiConsole.Confirm("¿Confirmar eliminación?", false))
        {
            Pause();
            return;
        }

        try
        {
            await _service.DeleteByIdAsync(id, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Eliminado correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task DeleteByNameAsync(CancellationToken cancellationToken)
    {
        var name = PromptRequiredText("Nombre a eliminar:");
        if (!AnsiConsole.Confirm("¿Confirmar eliminación?", false))
        {
            Pause();
            return;
        }

        try
        {
            await _service.DeleteByNameAsync(name, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Eliminado correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task DeleteByAgeAsync(CancellationToken cancellationToken)
    {
        var age = PromptNonNegativeInt("Edad en años (se eliminan todos los tipos cuyo rango la contenga):");
        if (!AnsiConsole.Confirm("¿Confirmar eliminación?", false))
        {
            Pause();
            return;
        }

        try
        {
            var n = await _service.DeleteByAgeAsync(age, cancellationToken);
            AnsiConsole.MarkupLine($"\n[green]Eliminados {n} registro(s).[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private static void RenderTable(IEnumerable<PassengerType> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No hay registros para mostrar.[/]");
            return;
        }

        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]ID[/]")
            .AddColumn("[bold grey]name[/]")
            .AddColumn("[bold grey]min_age[/]")
            .AddColumn("[bold grey]max_age[/]");

        foreach (var item in list)
        {
            table.AddRow(
                item.Id?.Value.ToString() ?? "-",
                item.Name.Value,
                item.MinAge.Value?.ToString() ?? "-",
                item.MaxAge.Value?.ToString() ?? "-");
        }

        AnsiConsole.Write(table);
    }

    private static int PromptPositiveInt(string label, int? current = null)
        => AnsiConsole.Prompt(new TextPrompt<int>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? 1)
            .Validate(v => v > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Debe ser mayor que cero.[/]")));

    private static int PromptNonNegativeInt(string label, int? current = null)
        => AnsiConsole.Prompt(new TextPrompt<int>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? 0)
            .Validate(v => v >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]No puede ser negativo.[/]")));

    private static int? PromptOptionalInt(string label, int? current = null)
    {
        var def = current.HasValue ? current.Value.ToString() : string.Empty;
        var text = AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(def)
            .AllowEmpty());
        if (string.IsNullOrWhiteSpace(text))
            return null;
        if (!int.TryParse(text.Trim(), out var v))
            throw new InvalidOperationException("Valor entero invalido.");
        return v;
    }

    private static string PromptRequiredText(string label, string? current = null)
        => AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? string.Empty)
            .Validate(v => string.IsNullOrWhiteSpace(v) ? ValidationResult.Error("[red]Obligatorio.[/]") : ValidationResult.Success())).Trim();

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Presiona Enter para continuar...[/]").AllowEmpty());
    }
}
