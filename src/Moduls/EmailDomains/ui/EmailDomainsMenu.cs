using Spectre.Console;
using GestorDeVuelosProyectoFinal.src.Moduls.EmailDomains.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.EmailDomains.ui;

public sealed class EmailDomainsMenu : IModuleUI
{
    private readonly IEmailDomainService _service;

    public string Key => "emaildomains";
    public string Title => "Email Domains";

    public EmailDomainsMenu(IEmailDomainService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Email Domains Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List email domains",
                        "Create email domain",
                        "Update email domain",
                        "Delete email domain",
                        "Back"));

            switch (option)
            {
                case "List email domains": await ListAsync(); break;
                case "Create email domain": await CreateAsync(); break;
                case "Update email domain": await UpdateAsync(); break;
                case "Delete email domain": await DeleteAsync(); break;
                case "Back": return;
            }
        }
    }

    private async Task ListAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Email Domains[/]").LeftJustified());

        var domains = (await _service.GetAllAsync()).ToList();

        if (domains.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No email domains registered yet.[/]");
        }
        else
        {
            var table = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Grey)
                .AddColumn(new TableColumn("[bold grey]ID[/]").Centered())
                .AddColumn(new TableColumn("[bold grey]Domain[/]"));

            foreach (var item in domains)
                table.AddRow(item.Id.Value.ToString(), $"[white]{item.Domain.Value}[/]");

            AnsiConsole.WriteLine();
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine($"\n[grey]Total: {domains.Count} domain(s)[/]");
        }

        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Email Domain[/]").LeftJustified());

        var domain = PromptDomain("Email domain:");

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[bold]{domain}[/]")
            .Header("[grey]Domain to create[/]")
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
                await _service.CreateAsync(domain);
                AnsiConsole.MarkupLine("\n[green]Email domain created successfully.[/]");
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

    private async Task UpdateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Update Email Domain[/]").LeftJustified());

        var domains = (await _service.GetAllAsync()).ToList();
        if (domains.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No email domains to update.[/]");
            Pause();
            return;
        }

        var choices = domains.Select(d => d.Domain.Value).ToList();
        choices.Add("Cancel");

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a domain to update:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(choices));

        if (selected == "Cancel") return;

        var newDomain = PromptDomain($"New domain for {selected}:");

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[strikethrough grey]{selected}[/]  [grey]->[/]  [bold]{newDomain}[/]")
            .Header("[grey]Change to apply[/]")
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
                await _service.UpdateAsync(selected, newDomain);
                AnsiConsole.MarkupLine("\n[green]Email domain updated successfully.[/]");
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

    private async Task DeleteAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Delete Email Domain[/]").LeftJustified());

        var domains = (await _service.GetAllAsync()).ToList();
        if (domains.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No email domains to delete.[/]");
            Pause();
            return;
        }

        var choices = domains.Select(d => d.Domain.Value).ToList();
        choices.Add("Cancel");

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Select a domain to delete:[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(choices));

        if (selected == "Cancel") return;

        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Panel($"[bold red]{selected}[/]")
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
                await _service.DeleteAsync(selected);
                AnsiConsole.MarkupLine("\n[green]Email domain deleted successfully.[/]");
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

    private static string PromptDomain(string label)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"[deepskyblue1]{label}[/]")
                .Validate(value =>
                {
                    var clean = value.Trim().ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(clean))
                        return ValidationResult.Error("[red]Domain cannot be empty.[/]");
                    if (clean.Length > 100)
                        return ValidationResult.Error("[red]Domain cannot exceed 100 characters.[/]");
                    if (!clean.Contains('.'))
                        return ValidationResult.Error("[red]Domain must include a dot. Example: gmail.com[/]");
                    if (clean.Contains('@') || clean.Contains(' '))
                        return ValidationResult.Error("[red]Domain must not contain '@' or spaces.[/]");
                    return ValidationResult.Success();
                }))
            .Trim()
            .ToLowerInvariant();
    }

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(
            new TextPrompt<string>("[grey]Press Enter to continue...[/]")
                .AllowEmpty());
    }
}
