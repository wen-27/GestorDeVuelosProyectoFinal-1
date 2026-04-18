using Spectre.Console;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using AircraftAggregate = GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.ui;

public sealed class AircraftMenu : IModuleUI
{
    private readonly IAircraftService _service;

    public AircraftMenu(IAircraftService service)
    {
        _service = service;
    }

    public string Key => "aircraft";
    public string Title => "Aeronaves";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Aeronaves [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Usa las flechas para navegar[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "Listar todas",
                        "Buscar por ID",
                        "Buscar por matrícula",
                        "Buscar por aerolínea",
                        "Crear aeronave",
                        "Actualizar aeronave",
                        "Desactivar aeronave",
                        "Volver"));

            switch (option)
            {
                case "Listar todas": await ListAllAsync(cancellationToken); break;
                case "Buscar por ID": await SearchByIdAsync(cancellationToken); break;
                case "Buscar por matrícula": await SearchByRegistrationAsync(cancellationToken); break;
                case "Buscar por aerolínea": await SearchByAirlineAsync(cancellationToken); break;
                case "Crear aeronave": await CreateAsync(cancellationToken); break;
                case "Actualizar aeronave": await UpdateAsync(cancellationToken); break;
                case "Desactivar aeronave": await DeactivateAsync(cancellationToken); break;
                case "Volver": return;
            }
        }
    }

    private async Task ListAllAsync(CancellationToken cancellationToken)
    {
        RenderTable(await _service.GetAllAsync(cancellationToken), "Todas las aeronaves");
        Pause();
    }

    private async Task SearchByIdAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID de la aeronave:");
        var item = await _service.GetByIdAsync(id, cancellationToken);
        RenderTable(item is null ? [] : [item], $"Resultado para ID {id}");
        Pause();
    }

    private async Task SearchByRegistrationAsync(CancellationToken cancellationToken)
    {
        var registration = PromptRequiredText("Matrícula:");
        var item = await _service.GetByRegistrationAsync(registration, cancellationToken);
        RenderTable(item is null ? [] : [item], $"Resultado para {registration}");
        Pause();
    }

    private async Task SearchByAirlineAsync(CancellationToken cancellationToken)
    {
        var airlineId = PromptPositiveInt("ID de la aerolínea:");
        RenderTable(await _service.GetByAirlineIdAsync(airlineId, cancellationToken), $"Aeronaves de la aerolínea {airlineId}");
        Pause();
    }

    private async Task CreateAsync(CancellationToken cancellationToken)
    {
        var form = PromptForm();
        try
        {
            await _service.CreateAsync(form.modelId, form.airlineId, form.registration, form.manufacturedDate, form.isActive, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Aeronave creada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private async Task UpdateAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID de la aeronave a actualizar:");
        var aircraft = await _service.GetByIdAsync(id, cancellationToken);

        if (aircraft is null)
        {
            AnsiConsole.MarkupLine("\n[yellow]No se encontró la aeronave.[/]");
            Pause();
            return;
        }

        var form = PromptForm(
            aircraft.ModelId.Value,
            aircraft.AirlineId.Value,
            aircraft.Registration.Value,
            aircraft.ManufacturedDate.Value,
            aircraft.IsActive.Value);

        try
        {
            await _service.UpdateAsync(id, form.modelId, form.airlineId, form.registration, form.manufacturedDate, form.isActive, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Aeronave actualizada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private async Task DeactivateAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID de la aeronave a desactivar:");
        if (!AnsiConsole.Confirm("¿Confirmas la desactivación?", false))
        {
            Pause();
            return;
        }

        try
        {
            await _service.DeactivateAsync(id, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Aeronave desactivada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private static void RenderTable(IEnumerable<AircraftAggregate> items, string title)
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
            .AddColumn("[bold grey]Modelo[/]")
            .AddColumn("[bold grey]Aerolínea[/]")
            .AddColumn("[bold grey]Matrícula[/]")
            .AddColumn("[bold grey]Fabricación[/]")
            .AddColumn("[bold grey]Activa[/]");

        foreach (var item in list)
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.ModelId.Value.ToString(),
                item.AirlineId.Value.ToString(),
                item.Registration.Value,
                item.ManufacturedDate.Value?.ToString("yyyy-MM-dd") ?? "-",
                item.IsActive.Value ? "Sí" : "No");
        }

        AnsiConsole.Write(table);
    }

    private static (int modelId, int airlineId, string registration, DateTime? manufacturedDate, bool isActive) PromptForm(
        int? currentModelId = null,
        int? currentAirlineId = null,
        string? currentRegistration = null,
        DateTime? currentManufacturedDate = null,
        bool? currentIsActive = null)
    {
        var modelId = PromptPositiveInt("ID del modelo:", currentModelId);
        var airlineId = PromptPositiveInt("ID de la aerolínea:", currentAirlineId);
        var registration = PromptRequiredText("Matrícula:", currentRegistration).ToUpperInvariant();
        var manufacturedDate = PromptOptionalDate("Fecha de fabricación (yyyy-MM-dd, opcional):", currentManufacturedDate);
        var isActive = AnsiConsole.Confirm("¿Está activa?", currentIsActive ?? true);
        return (modelId, airlineId, registration, manufacturedDate, isActive);
    }

    private static int PromptPositiveInt(string label, int? current = null)
        => AnsiConsole.Prompt(new TextPrompt<int>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? 1)
            .Validate(v => v > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Debe ser mayor que cero.[/]")));

    private static string PromptRequiredText(string label, string? current = null)
        => AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? string.Empty)
            .Validate(v => string.IsNullOrWhiteSpace(v) ? ValidationResult.Error("[red]Campo obligatorio.[/]") : ValidationResult.Success())).Trim();

    private static DateTime? PromptOptionalDate(string label, DateTime? current = null)
    {
        var text = AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current?.ToString("yyyy-MM-dd") ?? string.Empty)
            .AllowEmpty());
        return string.IsNullOrWhiteSpace(text) ? null : DateTime.Parse(text);
    }

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Presiona Enter para continuar...[/]").AllowEmpty());
    }
}
