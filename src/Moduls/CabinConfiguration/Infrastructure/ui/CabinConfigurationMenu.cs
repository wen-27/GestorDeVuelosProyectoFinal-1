using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using CabinConfigurationAggregate = GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Aggregate.CabinConfiguration;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinConfiguration.Infrastructure.ui;

public sealed class CabinConfigurationMenu : IModuleUI
{
    private readonly ICabinConfigurationService _service;

    public CabinConfigurationMenu(ICabinConfigurationService service)
    {
        _service = service;
    }

    public string Key => "cabin-configuration";
    public string Title => "Configuración de Cabina";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Configuración de Cabina [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Usa las flechas para navegar[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "Listar todas",
                        "Buscar por aeronave",
                        "Crear configuración",
                        "Actualizar configuración",
                        "Eliminar configuración",
                        "Volver"));

            switch (option)
            {
                case "Listar todas": await ListAllAsync(); break;
                case "Buscar por aeronave": await ListByAircraftAsync(); break;
                case "Crear configuración": await CreateAsync(); break;
                case "Actualizar configuración": await UpdateAsync(); break;
                case "Eliminar configuración": await DeleteAsync(); break;
                case "Volver": return;
            }
        }
    }

    private async Task ListAllAsync()
    {
        RenderTable(await _service.GetAllAsync(), "Configuraciones de cabina");
        Pause();
    }

    private async Task ListByAircraftAsync()
    {
        var aircraftId = PromptPositiveInt("ID de la aeronave:");
        RenderTable(await _service.GetByAircraftIdAsync(aircraftId), $"Configuraciones para aeronave {aircraftId}");
        Pause();
    }

    private async Task CreateAsync()
    {
        var form = PromptForm();
        try
        {
            await _service.CreateAsync(form.aircraftId, form.cabinTypeId, form.rowStart, form.rowEnd, form.seatsPerRow, form.seatLetters);
            AnsiConsole.MarkupLine("\n[green]Configuración creada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = PromptPositiveInt("ID de la configuración:");
        var current = await _service.GetByIdAsync(id);
        if (current is null)
        {
            AnsiConsole.MarkupLine("\n[yellow]No se encontró la configuración.[/]");
            Pause();
            return;
        }

        var form = PromptForm(
            current.AircraftId,
            current.CabinTypeId.Value,
            current.RowRange.StartRow,
            current.RowRange.EndRow,
            current.SeatsPerRow.Value,
            current.SeatLetters.Value);

        try
        {
            await _service.UpdateAsync(id, form.aircraftId, form.cabinTypeId, form.rowStart, form.rowEnd, form.seatsPerRow, form.seatLetters);
            AnsiConsole.MarkupLine("\n[green]Configuración actualizada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private async Task DeleteAsync()
    {
        var id = PromptPositiveInt("ID de la configuración a eliminar:");
        if (!AnsiConsole.Confirm("¿Confirmas la eliminación?", false))
        {
            Pause();
            return;
        }

        try
        {
            await _service.DeleteAsync(id);
            AnsiConsole.MarkupLine("\n[green]Configuración eliminada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{ex.Message}[/]");
        }
        Pause();
    }

    private static void RenderTable(IEnumerable<CabinConfigurationAggregate> items, string title)
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
            .AddColumn("[bold grey]Aeronave[/]")
            .AddColumn("[bold grey]Tipo[/]")
            .AddColumn("[bold grey]Filas[/]")
            .AddColumn("[bold grey]Asientos/Fila[/]")
            .AddColumn("[bold grey]Letras[/]")
            .AddColumn("[bold grey]Asientos generados[/]");

        foreach (var item in list.OrderBy(x => x.AircraftId).ThenBy(x => x.CabinTypeId.Value))
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.AircraftId.ToString(),
                item.CabinTypeId.Value.ToString(),
                $"{item.RowRange.StartRow}-{item.RowRange.EndRow}",
                item.SeatsPerRow.Value.ToString(),
                item.SeatLetters.Value,
                item.GenerateSeatDesignators().Count().ToString());
        }

        AnsiConsole.Write(table);
    }

    private static (int aircraftId, int cabinTypeId, int rowStart, int rowEnd, int seatsPerRow, string seatLetters) PromptForm(
        int? currentAircraftId = null,
        int? currentCabinTypeId = null,
        int? currentRowStart = null,
        int? currentRowEnd = null,
        int? currentSeatsPerRow = null,
        string? currentSeatLetters = null)
    {
        var aircraftId = PromptPositiveInt("ID de la aeronave:", currentAircraftId);
        var cabinTypeId = PromptPositiveInt("ID del tipo de cabina:", currentCabinTypeId);
        var rowStart = PromptPositiveInt("Fila inicial:", currentRowStart);
        var rowEnd = PromptPositiveInt("Fila final:", currentRowEnd);
        var seatsPerRow = PromptPositiveInt("Asientos por fila:", currentSeatsPerRow);
        var seatLetters = PromptRequiredText("Letras de asientos:", currentSeatLetters).ToUpperInvariant();
        return (aircraftId, cabinTypeId, rowStart, rowEnd, seatsPerRow, seatLetters);
    }

    private static int PromptPositiveInt(string label, int? current = null)
        => AnsiConsole.Prompt(new TextPrompt<int>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? 1)
            .Validate(v => v > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Debe ser mayor que cero.[/]")));

    private static string PromptRequiredText(string label, string? current = null)
        => AnsiConsole.Prompt(new TextPrompt<string>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? string.Empty)
            .Validate(v => string.IsNullOrWhiteSpace(v) ? ValidationResult.Error("[red]Campo obligatorio.[/]") : ValidationResult.Success())).Trim();

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Presiona Enter para continuar...[/]").AllowEmpty());
    }
}
