using Spectre.Console;
using GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.ui;

public sealed class AircraftMenu : IModuleUI
{
    private readonly IAircraftService _service;

    public string Key => "aircraft";
    public string Title => "Aircraft";

    public AircraftMenu(IAircraftService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Aircraft Management [/]").LeftJustified());

            // Nota guía:
            // La tabla aircraft ya tiene model_id y airline_id.
            // Este menú todavía no pide esos campos porque el aggregate/service actual
            // aún no los modela. Cuando subas esas relaciones al dominio, aquí es donde
            // debes pedir:
            // 1. Aircraft Model ID  (1 aircraft_model -> many aircraft)
            // 2. Airline ID         (1 airline -> many aircraft)
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List all aircraft",
                        "Create aircraft",
                        "Update aircraft",
                        "Delete aircraft",
                        "Back"));

            switch (option)
            {
                case "List all aircraft": await ListAllAsync(); break;
                case "Create aircraft": await CreateAsync(); break;
                case "Update aircraft": await UpdateAsync(); break;
                case "Delete aircraft": await DeleteAsync(); break;
                case "Back": return;
            }
        }
    }

    private async Task ListAllAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Aircraft[/]").LeftJustified());

        var aircrafts = (await _service.GetAllAsync()).ToList();
        RenderAircraftsTable(aircrafts);
        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Aircraft[/]").LeftJustified());

        var form = PromptAircraftData(includeId: true);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel(
                $"ID: [bold]{form.id}[/]\n" +
                $"Registration: [bold]{form.registration}[/]\n" +
                $"Manufactured date: [bold]{form.dateManufactured:yyyy-MM-dd}[/]\n" +
                $"Is active: [bold]{(form.isActive ? "Yes" : "No")}[/]")
            .Header("[grey]Aircraft to create[/]")
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
                    form.id,
                    form.registration,
                    form.dateManufactured,
                    form.isActive);

                AnsiConsole.MarkupLine("\n[green]Aircraft created successfully.[/]");
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
        AnsiConsole.Write(new Rule("[bold]Update Aircraft[/]").LeftJustified());

        var aircrafts = (await _service.GetAllAsync()).ToList();
        if (aircrafts.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No aircraft to update.[/]");
            Pause();
            return;
        }

        RenderAircraftsTable(aircrafts);
        AnsiConsole.WriteLine();

        var id = PromptPositiveInt("Aircraft ID to update:");
        var current = await _service.GetByIdAsync(id);

        if (current is null)
        {
            AnsiConsole.MarkupLine($"\n[red]Aircraft with id {id} was not found.[/]");
            Pause();
            return;
        }

        var form = PromptAircraftData(includeId: false);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel(
                $"ID: [bold]{id}[/]\n" +
                $"Registration: [bold]{current.Registration.Value}[/] [grey]->[/] [bold]{form.registration}[/]\n" +
                $"Manufactured date: [bold]{current.DateManufactured.Value:yyyy-MM-dd}[/] [grey]->[/] [bold]{form.dateManufactured:yyyy-MM-dd}[/]\n" +
                $"Is active: [bold]{(current.IsActive ? "Yes" : "No")}[/] [grey]->[/] [bold]{(form.isActive ? "Yes" : "No")}[/]")
            .Header("[grey]Changes to apply[/]")
            .BorderColor(Color.Grey));

        var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nConfirm?")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Yes, update", "No, cancel"));

        if (confirm == "Yes, update")
        {
            try
            {
                await _service.UpdateAsync(
                    id,
                    form.registration,
                    form.dateManufactured,
                    form.isActive);

                AnsiConsole.MarkupLine("\n[green]Aircraft updated successfully.[/]");
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

    private async Task DeleteAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold red]Delete Aircraft[/]").LeftJustified());

        var aircrafts = (await _service.GetAllAsync()).ToList();
        if (aircrafts.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No aircraft to delete.[/]");
            Pause();
            return;
        }

        RenderAircraftsTable(aircrafts);
        AnsiConsole.WriteLine();

        var id = PromptPositiveInt("Aircraft ID to delete:");
        var selected = await _service.GetByIdAsync(id);

        if (selected is null)
        {
            AnsiConsole.MarkupLine($"\n[red]Aircraft with id {id} was not found.[/]");
            Pause();
            return;
        }

        AnsiConsole.Write(new Panel(
                $"[bold red]ID:[/] {selected.Id.Value}\n" +
                $"Registration: {selected.Registration.Value}\n" +
                $"Manufactured date: {selected.DateManufactured.Value:yyyy-MM-dd}\n" +
                $"Is active: {(selected.IsActive ? "Yes" : "No")}")
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
                AnsiConsole.MarkupLine("\n[green]Aircraft deleted successfully.[/]");
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

    private static void RenderAircraftsTable(
        IReadOnlyCollection<GestorDeVuelosProyectoFinal.src.Moduls.Aircraft.Domain.Aggregate.Aircraft> aircrafts)
    {
        if (aircrafts.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No aircraft registered yet.[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]ID[/]")
            .AddColumn("[bold grey]Registration[/]")
            .AddColumn("[bold grey]Manufactured Date[/]")
            .AddColumn("[bold grey]Active[/]");

        foreach (var item in aircrafts.OrderBy(x => x.Id.Value))
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.Registration.Value,
                item.DateManufactured.Value.ToString("yyyy-MM-dd"),
                item.IsActive ? "[green]Yes[/]" : "[red]No[/]");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine($"\n[grey]Total: {aircrafts.Count} aircraft(s)[/]");
    }

    private static (int id, string registration, DateTime dateManufactured, bool isActive) PromptAircraftData(bool includeId)
    {
        var id = includeId ? PromptPositiveInt("Aircraft ID:") : 0;

        var registration = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Registration (example: N12345 or HK4521):[/]")
                .Validate(value =>
                {
                    var clean = value.Trim().ToUpperInvariant();
                    if (string.IsNullOrWhiteSpace(clean))
                        return ValidationResult.Error("[red]Registration is required.[/]");
                    if (clean.Length < 3 || clean.Length > 20)
                        return ValidationResult.Error("[red]Registration must be between 3 and 20 characters.[/]");
                    if (clean.Any(c => !char.IsLetterOrDigit(c) && c != '-'))
                        return ValidationResult.Error("[red]Only letters, numbers, and '-' are allowed.[/]");
                    return ValidationResult.Success();
                }))
            .Trim()
            .ToUpperInvariant();

        var dateManufactured = AnsiConsole.Prompt(
            new TextPrompt<DateTime>("[deepskyblue1]Manufactured date (yyyy-MM-dd):[/]")
                .PromptStyle("white")
                .ValidationErrorMessage("[red]Invalid date format.[/]")
                .Validate(value => value <= DateTime.Today
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Manufactured date cannot be in the future.[/]")));

        var isActiveText = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Is the aircraft active?[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Yes", "No"));

        return (id, registration, dateManufactured, isActiveText == "Yes");
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
        AnsiConsole.Prompt(
            new TextPrompt<string>("[grey]Press Enter to continue...[/]")
                .AllowEmpty());
    }
}
