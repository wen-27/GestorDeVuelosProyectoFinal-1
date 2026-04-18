using GestorDeVuelosProyectoFinal.Moduls.Personal.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.Personal.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.Personal.ui;

public sealed class PersonalMenu : IModuleUI
{
    private readonly IPersonalService _service;

    public string Key => "staff";
    public string Title => "🧑‍💼  Gestión de Personal";

    public PersonalMenu(IPersonalService service)
    {
        _service = service;
    }

    public async Task RunAsync(CancellationToken ct = default)
    {
        while (!ct.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule($"[yellow]{Title}[/]").RuleStyle("grey").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Seleccione una opción:")
                    .PageSize(14)
                    .AddChoices(new[]
                    {
                        "1. Listar todo el personal",
                        "2. Buscar por ID",
                        "3. Buscar por Person ID",
                        "4. Buscar por Nombre de Persona",
                        "5. Buscar por Cargo",
                        "6. Buscar por Aerolínea",
                        "7. Buscar por Aeropuerto",
                        "8. Buscar por Estado Activo",
                        "9. Registrar nuevo Empleado",
                        "10. Actualizar Empleado",
                        "11. Desactivar Empleado",
                        "12. Reactivar Empleado",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option.Split('.')[0])
            {
                case "1": await ListAllAsync(); break;
                case "2": await SearchByIdAsync(); break;
                case "3": await SearchByPersonIdAsync(); break;
                case "4": await SearchByPersonNameAsync(); break;
                case "5": await SearchByPositionAsync(); break;
                case "6": await SearchByAirlineAsync(); break;
                case "7": await SearchByAirportAsync(); break;
                case "8": await SearchByIsActiveAsync(); break;
                case "9": await CreateAsync(); break;
                case "10": await UpdateAsync(); break;
                case "11": await DeactivateMenuAsync(); break;
                case "12": await ReactivateAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todo el Personal");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del empleado:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún empleado con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByPersonIdAsync()
    {
        var personId = AnsiConsole.Ask<int>("Ingrese el [green]Person ID[/]:");
        var item = await _service.GetByPersonIdAsync(personId);

        if (item is null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún empleado con ese Person ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para Person ID: {personId}");

        Pause();
    }

    private async Task SearchByPersonNameAsync()
    {
        var personName = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] de la persona:");
        var items = await _service.GetByPersonNameAsync(personName);
        ShowSearchResults(items, $"Personal para nombre: {personName}");
    }

    private async Task SearchByPositionAsync()
    {
        var positionName = AnsiConsole.Ask<string>("Ingrese el [green]cargo[/]:");
        var items = await _service.GetByPositionNameAsync(positionName);
        ShowSearchResults(items, $"Personal con cargo: {positionName}");
    }

    private async Task SearchByAirlineAsync()
    {
        var airlineName = AnsiConsole.Ask<string>("Ingrese la [green]aerolínea[/]:");
        var items = await _service.GetByAirlineNameAsync(airlineName);
        ShowSearchResults(items, $"Personal en aerolínea: {airlineName}");
    }

    private async Task SearchByAirportAsync()
    {
        var airportName = AnsiConsole.Ask<string>("Ingrese el [green]aeropuerto[/]:");
        var items = await _service.GetByAirportNameAsync(airportName);
        ShowSearchResults(items, $"Personal en aeropuerto: {airportName}");
    }

    private async Task SearchByIsActiveAsync()
    {
        var isActive = AnsiConsole.Confirm("¿Buscar empleados activos?", true);
        var items = await _service.GetByIsActiveAsync(isActive);
        ShowSearchResults(items, isActive ? "Personal Activo" : "Personal Inactivo");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nuevo Empleado[/]");
        var personId = AnsiConsole.Ask<int>("Person ID:");
        var positionId = AnsiConsole.Ask<int>("Position ID:");
        var airlineId = AskOptionalInt("Airline ID (opcional, ENTER para omitir):");
        var airportId = AskOptionalInt("Airport ID (opcional, ENTER para omitir):");
        var hireDate = AnsiConsole.Ask<DateTime>("Hire Date (yyyy-MM-dd):");
        var isActive = AnsiConsole.Confirm("¿Registrar como activo?", true);

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(personId, positionId, airlineId, airportId, hireDate, isActive);
                AnsiConsole.MarkupLine("[green]✅ Empleado registrado exitosamente.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
            }
        }

        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] del empleado a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Empleado no encontrado.[/]");
            Pause();
            return;
        }

        var personId = AnsiConsole.Ask<int>("Nuevo Person ID:", item.PersonId.Value);
        var positionId = AnsiConsole.Ask<int>("Nuevo Position ID:", item.PositionId.Value);
        var airlineId = AskOptionalInt("Nuevo Airline ID:", item.AirlineId?.Value);
        var airportId = AskOptionalInt("Nuevo Airport ID:", item.AirportId?.Value);
        var hireDate = AnsiConsole.Ask<DateTime>("Nueva Hire Date:", item.HireDate.Value);
        var isActive = AnsiConsole.Confirm("¿Empleado activo?", item.IsActive.Value);

        try
        {
            await _service.UpdateAsync(id, personId, positionId, airlineId, airportId, hireDate, isActive);
            AnsiConsole.MarkupLine("[green]✅ Empleado actualizado.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeactivateMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Menú de Desactivación[/]")
                .AddChoices("Desactivar por ID", "Desactivar por Nombre", "Desactivar por Cargo", "Desactivar por Aerolínea", "Desactivar por Aeropuerto", "Cancelar"));

        if (subOption == "Cancelar")
        {
            Pause();
            return;
        }

        try
        {
            switch (subOption)
            {
                case "Desactivar por ID":
                    var id = AnsiConsole.Ask<int>("ID a desactivar:");
                    if (AnsiConsole.Confirm("¿Desea desactivar el empleado?"))
                        await _service.DeactivateByIdAsync(id);
                    break;
                case "Desactivar por Nombre":
                    var personName = AnsiConsole.Ask<string>("Nombre a desactivar:");
                    if (AnsiConsole.Confirm("¿Desea desactivar los empleados?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByPersonNameAsync(personName)}[/]");
                    break;
                case "Desactivar por Cargo":
                    var positionName = AnsiConsole.Ask<string>("Cargo a desactivar:");
                    if (AnsiConsole.Confirm("¿Desea desactivar los empleados?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByPositionNameAsync(positionName)}[/]");
                    break;
                case "Desactivar por Aerolínea":
                    var airlineName = AnsiConsole.Ask<string>("Aerolínea:");
                    if (AnsiConsole.Confirm("¿Desea desactivar los empleados?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByAirlineNameAsync(airlineName)}[/]");
                    break;
                case "Desactivar por Aeropuerto":
                    var airportName = AnsiConsole.Ask<string>("Aeropuerto:");
                    if (AnsiConsole.Confirm("¿Desea desactivar los empleados?"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros desactivados: {await _service.DeactivateByAirportNameAsync(airportName)}[/]");
                    break;
            }

            AnsiConsole.MarkupLine("[green]✅ Operación procesada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private async Task ReactivateAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del empleado a reactivar:");
        try
        {
            await _service.ReactivateAsync(id);
            AnsiConsole.MarkupLine("[green]✅ Empleado reactivado.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowSearchResults(IEnumerable<Staff> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]❌ No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<Staff> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Person ID[/]")
            .AddColumn("[blue]Position ID[/]")
            .AddColumn("[green]Airline ID[/]")
            .AddColumn("[blue]Airport ID[/]")
            .AddColumn("[green]Hire Date[/]")
            .AddColumn("[blue]Activo[/]");

        foreach (var item in items)
        {
            table.AddRow(
                (item.Id?.Value ?? 0).ToString(),
                item.PersonId.Value.ToString(),
                item.PositionId.Value.ToString(),
                item.AirlineId?.Value.ToString() ?? "-",
                item.AirportId?.Value.ToString() ?? "-",
                item.HireDate.Value.ToString("yyyy-MM-dd"),
                item.IsActive.Value ? "Sí" : "No");
        }

        AnsiConsole.Write(table);
    }

    private static int? AskOptionalInt(string prompt, int? currentValue = null)
    {
        var text = AnsiConsole.Ask<string>(prompt, currentValue?.ToString() ?? string.Empty);
        return string.IsNullOrWhiteSpace(text) ? null : int.Parse(text);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
