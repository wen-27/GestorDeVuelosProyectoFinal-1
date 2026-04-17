using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.Regions.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Regions.ui;

public sealed class RegionsMenu : IModuleUI
{
    private readonly IRegionService _service;

    public string Key => "regions";
    public string Title => "Regions";

    private static readonly string[] _regionTypes = new[]
    {
        "Departamento", "Estado", "Provincia",
        "Región", "Comunidad Autónoma", "Otro"
    };

    public RegionsMenu(IRegionService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Regions Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List all regions",
                        "Filter by country",
                        "Filter by type",
                        "Create region",
                        "Update region",
                        "Delete region",
                        "Back"
                    )
            );

            switch (option)
            {
                case "List all regions":   await ListAllAsync();         break;
                case "Filter by country":  await FilterByCountryAsync(); break;
                case "Filter by type":     await FilterByTypeAsync();    break;
                case "Create region":      await CreateAsync();          break;
                case "Update region":      await UpdateAsync();          break;
                case "Delete region":      await DeleteAsync();          break;
                case "Back":               return;
            }
        }
    }

    // LIST ALL

    private async Task ListAllAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Regions[/]").LeftJustified());

        var regions = (await _service.GetAllAsync()).ToList();

        if (regions.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No regions registered yet.[/]");
        }
        else
        {
            AnsiConsole.Write(BuildTable(regions));
            AnsiConsole.MarkupLine($"\n[grey]Total: {regions.Count} region(s)[/]");
        }

        Pause();
    }

    // FILTER BY COUNTRY

    private async Task FilterByCountryAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Filter by Country[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var countryId = AnsiConsole.Prompt(
            new TextPrompt<int>("[deepskyblue1]Enter country ID:[/]")
                .Validate(id => id > 0
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]ID must be greater than 0.[/]"))
        );

        var regions = (await _service.GetByCountryIdAsync(countryId)).ToList();

        if (regions.Count == 0)
            AnsiConsole.MarkupLine("\n[yellow]No regions found for that country.[/]");
        else
        {
            AnsiConsole.Write(BuildTable(regions));
            AnsiConsole.MarkupLine($"\n[grey]Total: {regions.Count} region(s)[/]");
        }

        Pause();
    }

 
    // FILTER BY TYPE
 
    private async Task FilterByTypeAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Filter by Type[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var typeChoices = _regionTypes.ToList();
        typeChoices.Add("Cancel");

        var selectedType = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a region type:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(typeChoices)
        );

        if (selectedType == "Cancel") return;

        var regions = (await _service.GetByTypeAsync(selectedType)).ToList();

        if (regions.Count == 0)
            AnsiConsole.MarkupLine($"\n[yellow]No regions of type '{selectedType}' found.[/]");
        else
        {
            AnsiConsole.Write(BuildTable(regions));
            AnsiConsole.MarkupLine($"\n[grey]Total: {regions.Count} region(s)[/]");
        }

        Pause();
    }


    // CREATE
    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Region[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Region name:[/]")
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

        var typeChoices = _regionTypes.ToList();
        var type = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select region type:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(typeChoices)
        );

        var countryId = AnsiConsole.Prompt(
            new TextPrompt<int>("[deepskyblue1]Country ID:[/]")
                .Validate(id => id > 0
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]ID must be greater than 0.[/]"))
        );

        AnsiConsole.WriteLine();
        var preview = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[grey]Field[/]")
            .AddColumn("[grey]Value[/]")
            .AddRow("Name",       $"[white]{name}[/]")
            .AddRow("Type",       $"[deepskyblue1]{type}[/]")
            .AddRow("Country ID", $"[white]{countryId}[/]");

        AnsiConsole.Write(preview);

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
                        await _service.CreateAsync(name, type, countryId));

                AnsiConsole.MarkupLine("\n[green]Region created successfully.[/]");
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
        AnsiConsole.Write(new Rule("[bold]Update Region[/]").LeftJustified());

        var regions = (await _service.GetAllAsync()).ToList();

        if (regions.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No regions to update.[/]");
            Pause();
            return;
        }

        var choices = regions.Select(r => $"{r.Id.Value}  —  {r.Name.Value}  [{r.Type.Value}]").ToList();
        choices.Add("Cancel");

        AnsiConsole.WriteLine();
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a region to update:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .PageSize(10)
                .AddChoices(choices)
        );

        if (selected == "Cancel") return;

        var regionId   = int.Parse(selected.Split("  —  ")[0].Trim());
        var currentName = selected.Split("  —  ")[1].Split("  [")[0].Trim();
        var currentType = selected.Split("  [")[1].TrimEnd(']');

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[grey]Editing:[/] [bold]{currentName}[/] [grey]({currentType})[/]");
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

        var typeChoices = _regionTypes.ToList();
        typeChoices.Insert(0, $"Keep current ({currentType})");

        var newTypeSelection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select new type:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(typeChoices)
        );

        var newCountryIdStr = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]New country ID[/] [grey](Enter to keep current):[/]")
                .AllowEmpty()
        );

        var finalName      = string.IsNullOrEmpty(newName) ? currentName : newName;
        var finalType      = newTypeSelection.StartsWith("Keep") ? currentType : newTypeSelection;
        var finalCountry   = int.TryParse(newCountryIdStr, out var parsedId) ? parsedId : 0;

        AnsiConsole.WriteLine();
        var preview = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[grey]Field[/]")
            .AddColumn("[grey]New value[/]")
            .AddRow("Name",       $"[white]{finalName}[/]")
            .AddRow("Type",       $"[deepskyblue1]{finalType}[/]")
            .AddRow("Country ID", finalCountry > 0 ? $"[white]{finalCountry}[/]" : "[grey]unchanged[/]");

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
                        await _service.UpdateAsync(regionId, finalName, finalType, finalCountry));

                AnsiConsole.MarkupLine("\n[green]Region updated successfully.[/]");
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
        AnsiConsole.Write(new Rule("[bold]Delete Region[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var method = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Delete by:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Name", "Type (deletes all of that type)", "Cancel")
        );

        if (method == "Cancel") return;

        if (method == "Name")
        {
            var regions = (await _service.GetAllAsync()).ToList();

            if (regions.Count == 0)
            {
                AnsiConsole.MarkupLine("\n[yellow]No regions to delete.[/]");
                Pause();
                return;
            }

            var choices = regions.Select(r => $"{r.Name.Value}  [{r.Type.Value}]").ToList();
            choices.Add("Cancel");

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[deepskyblue1]Select a region to delete:[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .PageSize(10)
                    .AddChoices(choices)
            );

            if (selected == "Cancel") return;

            var name = selected.Split("  [")[0].Trim();

            AnsiConsole.WriteLine();
            AnsiConsole.Write(new Panel($"[bold red]{name}[/]")
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
                            await _service.DeleteByNameAsync(name));

                    AnsiConsole.MarkupLine("\n[green]Region deleted successfully.[/]");
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
        }
        else
        {
            var typeChoices = _regionTypes.ToList();
            typeChoices.Add("Cancel");

            var selectedType = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[deepskyblue1]Select type to delete:[/]")
                    .HighlightStyle(new Style(foreground: Color.Red))
                    .AddChoices(typeChoices)
            );

            if (selectedType == "Cancel") return;

            AnsiConsole.WriteLine();
            AnsiConsole.MarkupLine($"[red]Warning:[/] This will delete [bold]ALL[/] regions of type [bold]{selectedType}[/].");

            var confirm = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nAre you sure?")
                    .HighlightStyle(new Style(foreground: Color.Red))
                    .AddChoices("Yes, delete all", "No, cancel")
            );

            if (confirm == "Yes, delete all")
            {
                try
                {
                    await AnsiConsole.Status()
                        .StartAsync("Deleting...", async _ =>
                            await _service.DeleteByTypeAsync(selectedType));

                    AnsiConsole.MarkupLine("\n[green]Regions deleted successfully.[/]");
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
        }

        Pause();
    }

 
    // HELPERS

    private static Table BuildTable(IEnumerable<GestorDeVuelosProyectoFinal.Moduls.Regions.Domain.Aggregate.Region> regions)
    {
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn(new TableColumn("[bold grey]ID[/]").Centered())
            .AddColumn(new TableColumn("[bold grey]Name[/]"))
            .AddColumn(new TableColumn("[bold grey]Type[/]"))
            .AddColumn(new TableColumn("[bold grey]Country ID[/]").Centered());

        foreach (var r in regions)
            table.AddRow(
                $"[grey]{r.Id.Value}[/]",
                $"[white]{r.Name.Value}[/]",
                $"[deepskyblue1]{r.Type.Value}[/]",
                $"[grey]{r.CountryId.Value}[/]"
            );

        return table;
    }

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(
            new TextPrompt<string>("[grey]Press Enter to continue...[/]")
                .AllowEmpty()
        );
    }
}