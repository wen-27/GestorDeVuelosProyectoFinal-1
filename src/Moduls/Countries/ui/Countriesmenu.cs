using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.Countries.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Countries.ui;

public sealed class CountriesMenu : IModuleUI
{
    private readonly ICountryService _service;

    public string Key => "countries";
    public string Title => "Countries";

    public CountriesMenu(ICountryService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Countries Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List all countries",
                        "Filter by continent",
                        "Create country",
                        "Update country",
                        "Delete country",
                        "Back"
                    )
            );

            switch (option)
            {
                case "List all countries":    await ListAllAsync();          break;
                case "Filter by continent":   await ListByContinentAsync();  break;
                case "Create country":        await CreateAsync();           break;
                case "Update country":        await UpdateAsync();           break;
                case "Delete country":        await DeleteAsync();           break;
                case "Back":                  return;
            }
        }
    }


    // LIST ALL


    private async Task ListAllAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Countries[/]").LeftJustified());

        var countries = (await _service.GetAllAsync()).ToList();

        if (countries.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No countries registered yet.[/]");
        }
        else
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Grey)
                .AddColumn(new TableColumn("[bold grey]#[/]").Centered())
                .AddColumn(new TableColumn("[bold grey]Name[/]"))
                .AddColumn(new TableColumn("[bold grey]ISO Code[/]").Centered());

            for (int i = 0; i < countries.Count; i++)
                table.AddRow(
                    $"[grey]{i + 1}[/]",
                    $"[white]{countries[i].Name.Value}[/]",
                    $"[deepskyblue1]{countries[i].IsoCode.Value}[/]"
                );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine($"\n[grey]Total: {countries.Count} country(s)[/]");
        }

        Pause();
    }


    // FILTER BY CONTINENT
    private async Task ListByContinentAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Filter by Continent[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var continentId = AnsiConsole.Prompt(
            new TextPrompt<int>("[deepskyblue1]Enter continent ID:[/]")
                .Validate(id => id > 0
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]ID must be greater than 0.[/]"))
        );

        var countries = (await _service.GetByContinentIdAsync(continentId)).ToList();

        if (countries.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No countries found for that continent.[/]");
        }
        else
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Grey)
                .AddColumn(new TableColumn("[bold grey]#[/]").Centered())
                .AddColumn(new TableColumn("[bold grey]Name[/]"))
                .AddColumn(new TableColumn("[bold grey]ISO Code[/]").Centered());

            for (int i = 0; i < countries.Count; i++)
                table.AddRow(
                    $"[grey]{i + 1}[/]",
                    $"[white]{countries[i].Name.Value}[/]",
                    $"[deepskyblue1]{countries[i].IsoCode.Value}[/]"
                );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine($"\n[grey]Total: {countries.Count} country(s)[/]");
        }

        Pause();
    }


    // CREATE


    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Country[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Country name:[/]")
                .Validate(n =>
                {
                    if (string.IsNullOrWhiteSpace(n))
                        return ValidationResult.Error("[red]Name cannot be empty.[/]");
                    if (n.Length < 2 || n.Length > 100)
                        return ValidationResult.Error("[red]Name must be between 2 and 100 characters.[/]");
                    if (n.All(char.IsDigit))
                        return ValidationResult.Error("[red]Name cannot contain only numbers.[/]");
                    return ValidationResult.Success();
                })
        );

        var isoCode = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]ISO code (2–3 letters, e.g. COL):[/]")
                .Validate(c =>
                {
                    var cleaned = c.Trim().ToUpper();
                    if (cleaned.Length < 2 || cleaned.Length > 3)
                        return ValidationResult.Error("[red]ISO code must be 2 or 3 letters.[/]");
                    if (!cleaned.All(char.IsLetter))
                        return ValidationResult.Error("[red]ISO code must contain only letters.[/]");
                    return ValidationResult.Success();
                })
        );

        var continentId = AnsiConsole.Prompt(
            new TextPrompt<int>("[deepskyblue1]Continent ID:[/]")
                .Validate(id => id > 0
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]ID must be greater than 0.[/]"))
        );

        AnsiConsole.WriteLine();
        var panel = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[grey]Field[/]")
            .AddColumn("[grey]Value[/]")
            .AddRow("Name", $"[white]{name}[/]")
            .AddRow("ISO Code", $"[deepskyblue1]{isoCode.ToUpper()}[/]")
            .AddRow("Continent ID", $"[white]{continentId}[/]");

        AnsiConsole.Write(panel);

        var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nConfirm?")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Yes, save", "No, cancel")
        );

        if (confirm == "Yes, save")
        {
            try
            {
                await AnsiConsole.Status()
                    .StartAsync("Saving...", async _ =>
                        await _service.CreateAsync(name, isoCode, continentId));

                AnsiConsole.MarkupLine("\n[green]Country created successfully.[/]");
            }
            catch (InvalidOperationException ex)
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


    // UPDATE


    private async Task UpdateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Update Country[/]").LeftJustified());

        var countries = (await _service.GetAllAsync()).ToList();

        if (countries.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No countries to update.[/]");
            Pause();
            return;
        }

        // Select by ISO code
        var choices = countries.Select(c => $"{c.IsoCode.Value}  —  {c.Name.Value}").ToList();
        choices.Add("Cancel");

        AnsiConsole.WriteLine();
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a country to update:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .PageSize(10)
                .AddChoices(choices)
        );

        if (selected == "Cancel") return;

        var currentIsoCode = selected.Split("  —  ")[0].Trim();
        var currentName    = selected.Split("  —  ")[1].Trim();

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[grey]Editing:[/] [bold]{currentName}[/] [grey]({currentIsoCode})[/]");
        AnsiConsole.WriteLine();

        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>($"[deepskyblue1]New name[/] [grey](Enter to keep '{currentName}'):[/]")
                .AllowEmpty()
                .Validate(n =>
                {
                    if (string.IsNullOrEmpty(n)) return ValidationResult.Success();
                    if (n.Length < 2 || n.Length > 100)
                        return ValidationResult.Error("[red]Name must be between 2 and 100 characters.[/]");
                    if (n.All(char.IsDigit))
                        return ValidationResult.Error("[red]Name cannot contain only numbers.[/]");
                    return ValidationResult.Success();
                })
        );

        var newIso = AnsiConsole.Prompt(
            new TextPrompt<string>($"[deepskyblue1]New ISO code[/] [grey](Enter to keep '{currentIsoCode}'):[/]")
                .AllowEmpty()
                .Validate(c =>
                {
                    if (string.IsNullOrEmpty(c)) return ValidationResult.Success();
                    var cleaned = c.Trim().ToUpper();
                    if (cleaned.Length < 2 || cleaned.Length > 3)
                        return ValidationResult.Error("[red]ISO code must be 2 or 3 letters.[/]");
                    if (!cleaned.All(char.IsLetter))
                        return ValidationResult.Error("[red]ISO code must contain only letters.[/]");
                    return ValidationResult.Success();
                })
        );

        var newContinentIdStr = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]New continent ID[/] [grey](Enter to keep current):[/]")
                .AllowEmpty()
        );

        var finalName       = string.IsNullOrEmpty(newName)       ? currentName      : newName;
        var finalIso        = string.IsNullOrEmpty(newIso)        ? currentIsoCode   : newIso.ToUpper();
        var finalContinent  = int.TryParse(newContinentIdStr, out var parsedId) ? parsedId : 0;

        AnsiConsole.WriteLine();
        var preview = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[grey]Field[/]")
            .AddColumn("[grey]New value[/]")
            .AddRow("Name",         $"[white]{finalName}[/]")
            .AddRow("ISO Code",     $"[deepskyblue1]{finalIso}[/]")
            .AddRow("Continent ID", finalContinent > 0 ? $"[white]{finalContinent}[/]" : "[grey]unchanged[/]");

        AnsiConsole.Write(preview);

        var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nConfirm?")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Yes, update", "No, cancel")
        );

        if (confirm == "Yes, update")
        {
            try
            {
                await AnsiConsole.Status()
                    .StartAsync("Updating...", async _ =>
                        await _service.UpdateAsync(currentIsoCode, finalName, finalIso, finalContinent));

                AnsiConsole.MarkupLine("\n[green]Country updated successfully.[/]");
            }
            catch (InvalidOperationException ex)
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


    // DELETE


    private async Task DeleteAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Delete Country[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var method = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Delete by:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Name", "ISO Code", "Cancel")
        );

        if (method == "Cancel") return;

        string target;

        if (method == "Name")
        {
            var countries = (await _service.GetAllAsync()).ToList();

            if (countries.Count == 0)
            {
                AnsiConsole.MarkupLine("\n[yellow]No countries to delete.[/]");
                Pause();
                return;
            }

            var choices = countries.Select(c => $"{c.Name.Value}  [{c.IsoCode.Value}]").ToList();
            choices.Add("Cancel");

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[deepskyblue1]Select a country to delete:[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .PageSize(10)
                    .AddChoices(choices)
            );

            if (selected == "Cancel") return;
            target = selected.Split("  [")[0].Trim();
        }
        else
        {
            target = AnsiConsole.Prompt(
                new TextPrompt<string>("[deepskyblue1]Enter ISO code:[/]")
                    .Validate(c =>
                    {
                        var cleaned = c.Trim().ToUpper();
                        return cleaned.Length is >= 2 and <= 3 && cleaned.All(char.IsLetter)
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]ISO code must be 2 or 3 letters.[/]");
                    })
            );
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[bold red]{target}[/]")
            .Header("[red]About to delete[/]")
            .BorderColor(Color.Red));
        AnsiConsole.MarkupLine("\n[red]Warning:[/] This action cannot be undone.");

        var confirm = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("\nAre you sure?")
                .HighlightStyle(new Style(foreground: Color.Red))
                .AddChoices("Yes, delete", "No, cancel")
        );

        if (confirm == "Yes, delete")
        {
            try
            {
                await AnsiConsole.Status()
                    .StartAsync("Deleting...", async _ =>
                    {
                        if (method == "Name")
                            await _service.DeleteByNameAsync(target);
                        else
                            await _service.DeleteByIsoCodeAsync(target);
                    });

                AnsiConsole.MarkupLine("\n[green]Country deleted successfully.[/]");
            }
            catch (InvalidOperationException ex)
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


    // HELPER


    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(
            new TextPrompt<string>("[grey]Press Enter to continue...[/]")
                .AllowEmpty()
        );
    }
}