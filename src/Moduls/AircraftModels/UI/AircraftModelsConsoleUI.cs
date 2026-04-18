using Spectre.Console;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.UI;

public sealed class AircraftModelsMenu : IModuleUI
{
    private readonly IAircraftModelsService _service;

    public AircraftModelsMenu(IAircraftModelsService service)
    {
        _service = service;
    }

    public string Key => "aircraft-models";
    public string Title => "Modelos de Aeronave";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Modelos de Aeronave [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Usa las flechas para navegar[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "Listar todos",
                        "Buscar por ID",
                        "Buscar por nombre",
                        "Buscar por fabricante",
                        "Crear modelo",
                        "Actualizar modelo",
                        "Eliminar modelo",
                        "Volver"));

            switch (option)
            {
                case "Listar todos": await ListAllAsync(cancellationToken); break;
                case "Buscar por ID": await SearchByIdAsync(cancellationToken); break;
                case "Buscar por nombre": await SearchByNameAsync(cancellationToken); break;
                case "Buscar por fabricante": await SearchByManufacturerAsync(cancellationToken); break;
                case "Crear modelo": await CreateAsync(cancellationToken); break;
                case "Actualizar modelo": await UpdateAsync(cancellationToken); break;
                case "Eliminar modelo": await DeleteAsync(cancellationToken); break;
                case "Volver": return;
            }
        }
    }

    private async Task ListAllAsync(CancellationToken cancellationToken)
    {
        RenderTable(await _service.GetAllAsync(cancellationToken), "Todos los modelos");
        Pause();
    }

    private async Task SearchByIdAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID del modelo:");
        var item = await _service.GetByIdAsync(id, cancellationToken);
        RenderTable(item is null ? [] : [item], $"Resultado para ID {id}");
        Pause();
    }

    private async Task SearchByNameAsync(CancellationToken cancellationToken)
    {
        var name = PromptRequiredText("Nombre del modelo:");
        var item = await _service.GetByNameAsync(name, cancellationToken);
        RenderTable(item is null ? [] : [item], $"Resultado para {name}");
        Pause();
    }

    private async Task SearchByManufacturerAsync(CancellationToken cancellationToken)
    {
        var manufacturerId = PromptPositiveInt("ID del fabricante:");
        RenderTable(await _service.GetByManufacturerIdAsync(manufacturerId, cancellationToken), $"Modelos del fabricante {manufacturerId}");
        Pause();
    }

    private async Task CreateAsync(CancellationToken cancellationToken)
    {
        var form = PromptForm();
        try
        {
            await _service.CreateAsync(form.manufacturerId, form.name, form.maxCapacity, form.weight, form.fuel, form.speed, form.altitude, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Modelo creado correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private async Task UpdateAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID del modelo a actualizar:");
        var model = await _service.GetByIdAsync(id, cancellationToken);

        if (model is null)
        {
            AnsiConsole.MarkupLine("\n[yellow]No se encontró el modelo.[/]");
            Pause();
            return;
        }

        var form = PromptForm(
            model.ManufacturerId.Value,
            model.ModelName.Value,
            model.MaxCapacity.Value,
            model.MaxTakeoffWeight.Value,
            model.FuelConsumption.Value,
            model.CruiseSpeed.Value,
            model.CruiseAltitude.Value);

        try
        {
            await _service.UpdateAsync(id, form.manufacturerId, form.name, form.maxCapacity, form.weight, form.fuel, form.speed, form.altitude, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Modelo actualizado correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private async Task DeleteAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID del modelo a eliminar:");
        if (!AnsiConsole.Confirm("¿Confirmas la eliminación?", false))
        {
            Pause();
            return;
        }

        try
        {
            await _service.DeleteAsync(id, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Modelo eliminado correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private static void RenderTable(IEnumerable<AircraftModel> items, string title)
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
            .AddColumn("[bold grey]Fabricante[/]")
            .AddColumn("[bold grey]Modelo[/]")
            .AddColumn("[bold grey]Capacidad[/]")
            .AddColumn("[bold grey]MTOW[/]")
            .AddColumn("[bold grey]Consumo[/]")
            .AddColumn("[bold grey]Velocidad[/]")
            .AddColumn("[bold grey]Altitud[/]");

        foreach (var item in list)
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.ManufacturerId.Value.ToString(),
                item.ModelName.Value,
                item.MaxCapacity.Value.ToString(),
                item.MaxTakeoffWeight.Value?.ToString("0.##") ?? "-",
                item.FuelConsumption.Value?.ToString("0.##") ?? "-",
                item.CruiseSpeed.Value?.ToString() ?? "-",
                item.CruiseAltitude.Value?.ToString() ?? "-");
        }

        AnsiConsole.Write(table);
    }

    private static (int manufacturerId, string name, int maxCapacity, decimal? weight, decimal? fuel, int? speed, int? altitude)
        PromptForm(
            int? currentManufacturerId = null,
            string? currentName = null,
            int? currentCapacity = null,
            decimal? currentWeight = null,
            decimal? currentFuel = null,
            int? currentSpeed = null,
            int? currentAltitude = null)
    {
        var manufacturerId = PromptPositiveInt("ID del fabricante:", currentManufacturerId);
        var name = PromptRequiredText("Nombre del modelo:", currentName);
        var maxCapacity = PromptPositiveInt("Capacidad máxima:", currentCapacity);
        var weight = PromptOptionalDecimal("MTOW kg (opcional):", currentWeight);
        var fuel = PromptOptionalDecimal("Consumo kg/h (opcional):", currentFuel);
        var speed = PromptOptionalInt("Velocidad km/h (opcional):", currentSpeed);
        var altitude = PromptOptionalInt("Altitud ft (opcional):", currentAltitude);
        return (manufacturerId, name, maxCapacity, weight, fuel, speed, altitude);
    }

    private static int PromptPositiveInt(string label, int? current = null)
        => AnsiConsole.Prompt(new TextPrompt<int>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? 1)
            .Validate(v => v > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Debe ser mayor que cero.[/]")));

    private static string PromptRequiredText(string label, string? current = null)
        => AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? string.Empty)
            .Validate(v => string.IsNullOrWhiteSpace(v) ? ValidationResult.Error("[red]Campo obligatorio.[/]") : ValidationResult.Success())).Trim();

    private static decimal? PromptOptionalDecimal(string label, decimal? current = null)
    {
        var text = AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current?.ToString() ?? string.Empty)
            .AllowEmpty());
        return string.IsNullOrWhiteSpace(text) ? null : decimal.Parse(text);
    }

    private static int? PromptOptionalInt(string label, int? current = null)
    {
        var text = AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current?.ToString() ?? string.Empty)
            .AllowEmpty());
        return string.IsNullOrWhiteSpace(text) ? null : int.Parse(text);
    }

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Presiona Enter para continuar...[/]").AllowEmpty());
    }
}
