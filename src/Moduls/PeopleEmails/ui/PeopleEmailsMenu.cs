using Spectre.Console;
using GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.PeopleEmails.ui;

public sealed class PeopleEmailsMenu : IModuleUI
{
    private readonly IPersonEmailsService _service;

    public string Key => "peopleemails";
    public string Title => "People Emails";

    public PeopleEmailsMenu(IPersonEmailsService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] People Emails Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List all people emails",
                        "List emails by person",
                        "Create person email",
                        "Update person email",
                        "Delete person email",
                        "Back"));

            switch (option)
            {
                case "List all people emails": await ListAllAsync(); break;
                case "List emails by person": await ListByPersonAsync(); break;
                case "Create person email": await CreateAsync(); break;
                case "Update person email": await UpdateAsync(); break;
                case "Delete person email": await DeleteAsync(); break;
                case "Back": return;
            }
        }
    }

    private async Task ListAllAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All People Emails[/]").LeftJustified());
        var emails = (await _service.GetAllAsync()).ToList();
        RenderTable(emails);
        Pause();
    }

    private async Task ListByPersonAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Emails By Person[/]").LeftJustified());
        var personId = PromptPositiveInt("Person ID:");
        var emails = (await _service.GetByPersonIdAsync(personId)).ToList();
        RenderTable(emails);
        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Create Person Email[/]").LeftJustified());
        var form = PromptForm(includeId: false);

        try
        {
            await _service.CreateAsync(form.personId, form.emailUser, form.emailDomainId, form.isPrimary);
            AnsiConsole.MarkupLine("\n[green]Person email created successfully.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]Error: {ex.Message}[/]");
        }

        Pause();
    }

    private async Task UpdateAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]Update Person Email[/]").LeftJustified());

        var emails = (await _service.GetAllAsync()).ToList();
        if (emails.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No person emails to update.[/]");
            Pause();
            return;
        }

        RenderTable(emails);
        var id = PromptPositiveInt("Person email ID to update:");
        var current = await _service.GetByIdAsync(id);

        if (current is null)
        {
            AnsiConsole.MarkupLine($"\n[red]Person email with id {id} was not found.[/]");
            Pause();
            return;
        }

        var form = PromptForm(includeId: false);

        try
        {
            await _service.UpdateAsync(id, form.personId, form.emailUser, form.emailDomainId, form.isPrimary);
            AnsiConsole.MarkupLine("\n[green]Person email updated successfully.[/]");
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
        AnsiConsole.Write(new Rule("[bold red]Delete Person Email[/]").LeftJustified());

        var emails = (await _service.GetAllAsync()).ToList();
        if (emails.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No person emails to delete.[/]");
            Pause();
            return;
        }

        RenderTable(emails);
        var id = PromptPositiveInt("Person email ID to delete:");

        try
        {
            await _service.DeleteAsync(id);
            AnsiConsole.MarkupLine("\n[green]Person email deleted successfully.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]Error: {ex.Message}[/]");
        }

        Pause();
    }

    private static void RenderTable(IReadOnlyCollection<GestorDeVuelosProyectoFinal.Moduls.PersonEmails.Domain.Aggregate.PersonEmail> emails)
    {
        if (emails.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No people emails registered yet.[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]ID[/]")
            .AddColumn("[bold grey]Person[/]")
            .AddColumn("[bold grey]Email User[/]")
            .AddColumn("[bold grey]Domain ID[/]")
            .AddColumn("[bold grey]Primary[/]");

        foreach (var item in emails.OrderBy(x => x.PersonId).ThenByDescending(x => x.IsPrimary.Value))
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.PersonId.ToString(),
                item.UserEmail.Value,
                item.EmailDomainId.Value.ToString(),
                item.IsPrimary.Value ? "[green]Yes[/]" : "[grey]No[/]");
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
    }

    private static (int personId, string emailUser, int emailDomainId, bool isPrimary) PromptForm(bool includeId)
    {
        var personId = PromptPositiveInt("Person ID:");
        var emailUser = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Email user (before @):[/]")
                .Validate(value =>
                {
                    var clean = value.Trim().ToLowerInvariant();
                    if (string.IsNullOrWhiteSpace(clean))
                        return ValidationResult.Error("[red]Email user is required.[/]");
                    if (clean.Contains("@") || clean.Contains(' '))
                        return ValidationResult.Error("[red]Do not include '@' or spaces.[/]");
                    return ValidationResult.Success();
                }))
            .Trim()
            .ToLowerInvariant();

        var emailDomainId = PromptPositiveInt("Email domain ID:");

        var isPrimaryText = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Is this the primary email?[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Yes", "No"));

        return (personId, emailUser, emailDomainId, isPrimaryText == "Yes");
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
