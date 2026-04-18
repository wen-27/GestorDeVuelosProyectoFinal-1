using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.CabinTypes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.CabinTypes.UI;

public sealed class CabinTypesMenu : IModuleUI
{
    private readonly ICabinTypeService _service;

    public string Key => "cabintypes";
    public string Title => "Cabin Types";

    public CabinTypesMenu(ICabinTypeService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Cabin Types Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices("List all cabin types", "Create cabin type", "Delete cabin type", "Back")
            );

            switch (option)
            {
                case "List all cabin types": await ListAsync(); break;
                case "Create cabin type":    await CreateAsync(); break;
                case "Delete cabin type":    await DeleteAsync(); break;
                case "Back":                 return;
            }
        }
    }

    private async Task ListAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Cabin Types[/]").LeftJustified());

        var cabinTypes = (await _service.GetAllAsync()).ToList();

        if (!cabinTypes.Any())
        {
            AnsiConsole.MarkupLine("\n[yellow]No cabin types registered yet.[/]");
        }
        else
        {
            var table = new Table().Border(TableBorder.Rounded).BorderColor(Color.Grey);
            table.AddColumn(new TableColumn("[bold grey]ID[/]").Centered());
            table.AddColumn(new TableColumn("[bold grey]Name[/]"));

            foreach (var item in cabinTypes)
            {
                // Usamos el .Value o el .ToString() que añadiste
                table.AddRow(item.Id.ToString(), $"[white]{item.Name}[/]");
            }

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
        }
        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Cabin Type[/]").LeftJustified());

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Enter cabin type name (e.g. Business):[/]")
                .Validate(n => n.Length >= 2 ? ValidationResult.Success() : ValidationResult.Error("[red]Too short![/]"))
        );

        try
        {
            await _service.CreateAsync(name);
            AnsiConsole.MarkupLine("\n[green]✔ Cabin type created successfully.[/]");
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
        AnsiConsole.Write(new Rule("[bold red]Delete Cabin Type[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var cabinTypes = (await _service.GetAllAsync()).ToList();
        if (!cabinTypes.Any()) { AnsiConsole.MarkupLine("[yellow]Empty list.[/]"); Pause(); return; }

        var method = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Delete by:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Name", "ID", "Cancel")
        );

        if (method == "Cancel") return;

        string target;

        if (method == "Name")
        {
            var CabinTypes = (await _service.GetAllAsync()).ToList();

            if (cabinTypes.Count == 0)
            {
                AnsiConsole.MarkupLine("\n[yellow]No cabin types to delete.[/]");
                Pause();
                return;
            }

            var choices = cabinTypes.Select(c => $"{c.Name.Value}  [{c.Id.Value}]").ToList();
            choices.Add("Cancel");

            var selected = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a cabin type to delete:[/]")
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
                new TextPrompt<string>("[deepskyblue1]Enter cabin type ID:[/]")
                    .Validate(c =>
                    {
                        var cleaned = c.Trim().ToUpper();
                        return cleaned.Length is >= 2 and <= 3 && cleaned.All(char.IsLetter)
                            ? ValidationResult.Success()
                            : ValidationResult.Error("[red]ID must be between 2 and 30 characters.[/]");
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
                            await _service.DeleteByIdAsync(int.Parse(target));
                    });

                AnsiConsole.MarkupLine("\n[green]Cabin type deleted successfully.[/]");
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

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(
            new TextPrompt<string>("[grey]Press Enter to continue...[/]")
                .AllowEmpty()
        );
    }
}
