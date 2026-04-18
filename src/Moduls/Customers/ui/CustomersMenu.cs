using GestorDeVuelosProyectoFinal.src.Moduls.Customers.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.src.Moduls.Customers.ui;

public sealed class CustomersMenu : IModuleUI
{
    private readonly ICustomersService _service;

    public string Key => "customers";
    public string Title => "Customers";

    public CustomersMenu(ICustomersService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Customers Management [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Use arrows to navigate, Enter to select[/]")
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "List customers",
                        "Find customer by id",
                        "Find customer by person id",
                        "Find customer by document number",
                        "Create customer",
                        "Delete customer",
                        "Back"));

            switch (option)
            {
                case "List customers": await ListAsync(); break;
                case "Find customer by id": await FindByIdAsync(); break;
                case "Find customer by person id": await FindByPersonIdAsync(); break;
                case "Find customer by document number": await FindByDocumentAsync(); break;
                case "Create customer": await CreateAsync(); break;
                case "Delete customer": await DeleteAsync(); break;
                case "Back": return;
            }
        }
    }

    private async Task ListAsync()
    {
        AnsiConsole.Clear();
        AnsiConsole.Write(new Rule("[bold]All Customers[/]").LeftJustified());
        var customers = (await _service.GetAllAsync()).ToList();
        Render(customers);
        Pause();
    }

    private async Task FindByIdAsync()
    {
        var id = PromptPositiveInt("Customer ID:");
        var customer = await _service.GetByIdAsync(id);
        RenderSingle(customer, $"Customer with id {id} was not found.");
    }

    private async Task FindByPersonIdAsync()
    {
        var personId = PromptPositiveInt("Person ID:");
        var customer = await _service.GetByPersonIdAsync(personId);
        RenderSingle(customer, $"Customer with person id {personId} was not found.");
    }

    private async Task FindByDocumentAsync()
    {
        var documentNumber = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Document number:[/]")
                .Validate(value => string.IsNullOrWhiteSpace(value)
                    ? ValidationResult.Error("[red]Document number is required.[/]")
                    : ValidationResult.Success()));

        var customer = await _service.GetByDocumentNumberAsync(documentNumber);
        RenderSingle(customer, $"Customer with document number '{documentNumber}' was not found.");
    }

    private async Task CreateAsync()
    {
        var personId = PromptPositiveInt("Person ID to register as customer:");

        try
        {
            await _service.CreateAsync(personId);
            AnsiConsole.MarkupLine("\n[green]Customer created successfully.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]Error: {ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeleteAsync()
    {
        var id = PromptPositiveInt("Customer ID to delete:");

        try
        {
            await _service.DeleteAsync(id);
            AnsiConsole.MarkupLine("\n[green]Customer deleted successfully.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]Error: {ex.Message}[/]");
        }

        Pause();
    }

    private static void Render(IReadOnlyCollection<GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.Aggregate.Customer> customers)
    {
        if (customers.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No customers registered yet.[/]");
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]ID[/]")
            .AddColumn("[bold grey]Person ID[/]")
            .AddColumn("[bold grey]Created At[/]");

        foreach (var item in customers.OrderBy(x => x.Id.Value))
        {
            table.AddRow(
                item.Id.Value.ToString(),
                item.PersonId.ToString(),
                item.CreatedAt.Value.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.Write(table);
    }

    private static void RenderSingle(GestorDeVuelosProyectoFinal.src.Moduls.Customers.Domain.Aggregate.Customer? customer, string notFoundMessage)
    {
        if (customer is null)
        {
            AnsiConsole.MarkupLine($"\n[yellow]{notFoundMessage}[/]");
            Pause();
            return;
        }

        var panel = new Panel(
            $"ID: [bold]{customer.Id.Value}[/]\n" +
            $"Person ID: [bold]{customer.PersonId}[/]\n" +
            $"Created At: [bold]{customer.CreatedAt.Value:yyyy-MM-dd HH:mm:ss}[/]")
            .Header("[grey]Customer[/]")
            .BorderColor(Color.Grey);

        AnsiConsole.WriteLine();
        AnsiConsole.Write(panel);
        Pause();
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
