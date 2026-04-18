using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinConfiguration.Infrastructure.ui;

public sealed class CabinConfigurationMenu : IModuleUI
{
    private readonly ICabinConfigurationService _service;

    public string Key => "cabinconfiguration";
    public string Title => "Cabin Configuration";

    public CabinConfigurationMenu(ICabinConfigurationService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Cabin Configuration Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List all cabin configurations",
                        "List configurations by aircraft",
                        "Create cabin configuration",
                        "Update cabin configuration",
                        "Delete cabin configuration",
                        "Back"));

            switch (option)
            {
                case "List all cabin configurations": await ListAllAsync(); break;
                case "List configurations by aircraft": await ListByAircraftAsync(); break;
                case "Create cabin configuration": await CreateAsync(); break;
                case "Update cabin configuration": await UpdateAsync(); break;
                case "Delete cabin configuration": await DeleteAsync(); break;
                case "Back": return;
            }
        }
    }

    private async Task ListAllAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Cabin Configurations[/]").LeftJustified());

        var configurations = (await _service.GetAllAsync()).ToList();
        RenderConfigurationsTable(configurations);
        Pause();
    }

    private async Task ListByAircraftAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Cabin Configurations By Aircraft[/]").LeftJustified());

        var aircraftId = PromptPositiveInt("Aircraft ID:");
        var configurations = (await _service.GetByAircraftIdAsync(aircraftId)).ToList();

        if (configurations.Count == 0)
            AnsiConsole.MarkupLine($"\n[yellow]No configurations found for aircraft {aircraftId}.[/]");
        else
            RenderConfigurationsTable(configurations);

        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Cabin Configuration[/]").LeftJustified());

        var form = PromptConfigurationData();

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel(
                $"Aircraft: [bold]{form.aircraftId}[/]\n" +
                $"Cabin type: [bold]{form.cabinTypeId}[/]\n" +
                $"Rows: [bold]{form.rowStart} - {form.rowEnd}[/]\n" +
                $"Seats/row: [bold]{form.seatsPerRow}[/]\n" +
                $"Seat letters: [bold]{form.seatLetters}[/]")
            .Header("[grey]Configuration to create[/]")
            .BorderColor(Color.Grey));

        var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nConfirm?")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Yes, save", "No, cancel"));

        if (confirm == "Yes, save")
        {
            try
            {
                await _service.CreateAsync(
                    form.aircraftId,
                    form.cabinTypeId,
                    form.rowStart,
                    form.rowEnd,
                    form.seatsPerRow,
                    form.seatLetters);

                AnsiConsole.MarkupLine("\n[green]Cabin configuration created successfully.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"\n[red]Error: {ex.Message}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[yellow]Operation cancelled.[/]");
        }

        Pause();
    }

    private async Task UpdateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Update Cabin Configuration[/]").LeftJustified());

        var existing = (await _service.GetAllAsync()).ToList();
        if (existing.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No cabin configurations to update.[/]");
            Pause();
            return;
        }

        RenderConfigurationsTable(existing);
        AnsiConsole.WriteLine();

        var id = PromptPositiveInt("Configuration ID to update:");
        var current = await _service.GetByIdAsync(id);

        if (current is null)
        {
            AnsiConsole.MarkupLine($"\n[red]Configuration with id {id} was not found.[/]");
            Pause();
            return;
        }

        var form = PromptConfigurationData();

        try
        {
            await _service.UpdateAsync(
                id,
                form.aircraftId,
                form.cabinTypeId,
                form.rowStart,
                form.rowEnd,
                form.seatsPerRow,
                form.seatLetters);

            AnsiConsole.MarkupLine("\n[green]Cabin configuration updated successfully.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]Error: {ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeleteAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold red]Delete Cabin Configuration[/]").LeftJustified());

        var configurations = (await _service.GetAllAsync()).ToList();
        if (configurations.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No cabin configurations to delete.[/]");
            Pause();
            return;
        }

        RenderConfigurationsTable(configurations);
        AnsiConsole.WriteLine();

        var id = PromptPositiveInt("Configuration ID to delete:");
        var selected = await _service.GetByIdAsync(id);

        if (selected is null)
        {
            AnsiConsole.MarkupLine($"\n[red]Configuration with id {id} was not found.[/]");
            Pause();
            return;
        }

        AnsiConsole.Write(new Panel(
                $"[bold red]ID:[/] {selected.Id.Value}\n" +
                $"Aircraft: {selected.AircraftId}\n" +
                $"Cabin type: {selected.CabinTypeId.Value}\n" +
                $"Rows: {selected.RowRange.StartRow} - {selected.RowRange.EndRow}")
            .Header("[red]About to delete[/]")
            .BorderColor(Color.Red));

        var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nAre you sure?")
                .HighlightStyle(new Style(foreground: Color.Red))
                .AddChoices("Yes, delete", "No, cancel"));

        if (confirm == "Yes, delete")
        {
            try
            {
                await _service.DeleteAsync(id);
                AnsiConsole.MarkupLine("\n[green]Cabin configuration deleted successfully.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"\n[red]Error: {ex.Message}[/]");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[yellow]Operation cancelled.[/]");
        }

        Pause();
    }

    private static void RenderConfigurationsTable(
        IReadOnlyCollection<GestorDeVuelosProyectoFinal.Moduls.CabinConfiguration.Domain.Aggregate.CabinConfiguration> configurations)
    {
        if (configurations.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No cabin configurations registered yet.[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]ID[/]")
            .AddColumn("[bold grey]Aircraft[/]")
            .AddColumn("[bold grey]Cabin Type[/]")
            .AddColumn("[bold grey]Rows[/]")
            .AddColumn("[bold grey]Seats/Row[/]")
            .AddColumn("[bold grey]Letters[/]");

        foreach (var item in configurations.OrderBy(x => x.AircraftId).ThenBy(x => x.CabinTypeId.Value))
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.AircraftId.ToString(),
                item.CabinTypeId.Value.ToString(),
                $"{item.RowRange.StartRow}-{item.RowRange.EndRow}",
                item.SeatsPerRow.Value.ToString(),
                item.SeatLetters.Value);
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine($"\n[grey]Total: {configurations.Count} configuration(s)[/]");
    }

    private static (int aircraftId, int cabinTypeId, int rowStart, int rowEnd, int seatsPerRow, string seatLetters)
        PromptConfigurationData()
    {
        var aircraftId = PromptPositiveInt("Aircraft ID:");
        var cabinTypeId = PromptPositiveInt("Cabin type ID:");
        var rowStart = PromptPositiveInt("Start row:");
        var rowEnd = PromptPositiveInt("End row:");
        var seatsPerRow = PromptPositiveInt("Seats per row:");

        var seatLetters = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Seat letters (example: ABCDEF):[/]")
                .Validate(value =>
                {
                    var clean = value.Trim().ToUpperInvariant();
                    if (string.IsNullOrWhiteSpace(clean))
                        return ValidationResult.Error("[red]Seat letters are required.[/]");
                    if (clean.Any(c => !char.IsLetter(c)))
                        return ValidationResult.Error("[red]Only letters are allowed.[/]");
                    return ValidationResult.Success();
                }))
            .Trim()
            .ToUpperInvariant();

        return (aircraftId, cabinTypeId, rowStart, rowEnd, seatsPerRow, seatLetters);
    }

    private static int PromptPositiveInt(string label)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>($"[deepskyblue1]{label}[/]")
                .Validate(value => value > 0
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Value must be greater than zero.[/]")));
    }

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Press Enter to continue...[/]").AllowEmpty());
    }
}
