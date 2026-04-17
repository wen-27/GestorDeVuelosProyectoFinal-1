using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.Continents.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Continents.ui;

public sealed class ContinentsMenu : IModuleUI
{
    private readonly IContinentService _service;

    public string Key => "continents";
    public string Title => "Continents";

    public ContinentsMenu(IContinentService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Continents Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List continents",
                        "Create continent",
                        "Update continent",
                        "Delete continent",
                        "Back"
                    )
            );

            switch (option)
            {
                case "List continents":   await ListAsync();   break;
                case "Create continent":  await CreateAsync(); break;
                case "Update continent":  await UpdateAsync(); break;
                case "Delete continent":  await DeleteAsync(); break;
                case "Back":              return;
            }
        }
    }


    // LIST


    private async Task ListAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Continents[/]").LeftJustified());

        var continents = (await _service.GetAllAsync()).ToList();

        if (continents.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No continents registered yet.[/]");
        }
        else
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Grey)
                .AddColumn(new TableColumn("[bold grey]#[/]").Centered())
                .AddColumn(new TableColumn("[bold grey]Name[/]"));

            for (int i = 0; i < continents.Count; i++)
                table.AddRow(
                    $"[grey]{i + 1}[/]",
                    $"[white]{continents[i].Name.Value}[/]"
                );

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine($"\n[grey]Total: {continents.Count} continent(s)[/]");
        }

        Pause();
    }
 // CREATE


    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Continent[/]").LeftJustified());
        AnsiConsole.WriteLine();

        var name = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Continent name:[/]")
                .Validate(n =>
                {
                    if (string.IsNullOrWhiteSpace(n))
                        return ValidationResult.Error("[red]Name cannot be empty.[/]");
                    if (n.Length < 2 || n.Length > 50)
                        return ValidationResult.Error("[red]Name must be between 2 and 50 characters.[/]");
                    if (n.All(char.IsDigit))
                        return ValidationResult.Error("[red]Name cannot contain only numbers.[/]");
                    return ValidationResult.Success();
                })
        );

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[bold]{name}[/]")
            .Header("[grey]Continent to create[/]")
            .BorderColor(Color.Grey));

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
                    {
                        await _service.CreateAsync(name);
                    });

                AnsiConsole.MarkupLine("\n[green]Continent created successfully.[/]");
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
        AnsiConsole.Write(new Rule("[bold]Update Continent[/]").LeftJustified());

        var continents = (await _service.GetAllAsync()).ToList();

        if (continents.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No continents to update.[/]");
            Pause();
            return;
        }

        var choices = continents.Select(c => c.Name.Value).ToList();
        choices.Add("Cancel");

        AnsiConsole.WriteLine();
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a continent to update:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(choices)
        );

        if (selected == "Cancel") return;

        AnsiConsole.WriteLine();
        var newName = AnsiConsole.Prompt(
            new TextPrompt<string>($"[deepskyblue1]New name for[/] [bold]{selected}[/][deepskyblue1]:[/]")
                .Validate(n =>
                {
                    if (string.IsNullOrWhiteSpace(n))
                        return ValidationResult.Error("[red]Name cannot be empty.[/]");
                    if (n.Length < 2 || n.Length > 50)
                        return ValidationResult.Error("[red]Name must be between 2 and 50 characters.[/]");
                    if (n.All(char.IsDigit))
                        return ValidationResult.Error("[red]Name cannot contain only numbers.[/]");
                    return ValidationResult.Success();
                })
        );

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[strikethrough grey]{selected}[/]  [grey]→[/]  [bold]{newName}[/]")
            .Header("[grey]Change to apply[/]")
            .BorderColor(Color.Grey));

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
                    {
                        await _service.UpdateAsync(selected, newName);
                    });

                AnsiConsole.MarkupLine("\n[green]Continent updated successfully.[/]");
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
        AnsiConsole.Write(new Rule("[bold]Delete Continent[/]").LeftJustified());

        var continents = (await _service.GetAllAsync()).ToList();

        if (continents.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No continents to delete.[/]");
            Pause();
            return;
        }

        var choices = continents.Select(c => c.Name.Value).ToList();
        choices.Add("Cancel");

        AnsiConsole.WriteLine();
        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a continent to delete:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(choices)
        );

        if (selected == "Cancel") return;

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[bold red]{selected}[/]")
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
                        await _service.DeleteAsync(selected);
                    });

                AnsiConsole.MarkupLine("\n[green]Continent deleted successfully.[/]");
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