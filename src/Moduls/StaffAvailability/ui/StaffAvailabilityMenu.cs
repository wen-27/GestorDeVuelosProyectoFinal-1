using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.StaffAvailability.ui;

public sealed class StaffAvailabilityMenu : IModuleUI
{
    private readonly IStaffAvailabilityService _service;

    public string Key => "staff_availability";
    public string Title => "Gestion de Disponibilidad del Personal";

    public StaffAvailabilityMenu(IStaffAvailabilityService service)
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
                    .Title("Seleccione una opcion:")
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "1. Listar todas las disponibilidades",
                        "2. Buscar por ID",
                        "3. Buscar por Staff ID",
                        "4. Buscar por rango de fechas",
                        "5. Registrar disponibilidad",
                        "6. Actualizar disponibilidad",
                        "7. Eliminar disponibilidad",
                        "0. Volver al menu principal"
                    }));

            if (option.StartsWith("0"))
                break;

            switch (option.Split('.')[0])
            {
                case "1": await ListAllAsync(); break;
                case "2": await SearchByIdAsync(); break;
                case "3": await SearchByStaffIdAsync(); break;
                case "4": await SearchByDateRangeAsync(); break;
                case "5": await CreateAsync(); break;
                case "6": await UpdateAsync(); break;
                case "7": await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Disponibilidad del Personal");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la disponibilidad:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
            AnsiConsole.MarkupLine("[red]No se encontro ninguna disponibilidad con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByStaffIdAsync()
    {
        var staffId = AnsiConsole.Ask<int>("Ingrese el [green]Staff ID / Personal ID[/]:");
        var items = await _service.GetByStaffIdAsync(staffId);
        ShowSearchResults(items, $"Disponibilidades para staff #{staffId}");
    }

    private async Task SearchByDateRangeAsync()
    {
        var startsAt = AnsiConsole.Ask<DateTime>("Fecha inicial (yyyy-MM-dd HH:mm):");
        var endsAt = AnsiConsole.Ask<DateTime>("Fecha final (yyyy-MM-dd HH:mm):");
        var items = await _service.GetByDateRangeAsync(startsAt, endsAt);
        ShowSearchResults(items, $"Disponibilidades entre {startsAt:yyyy-MM-dd HH:mm} y {endsAt:yyyy-MM-dd HH:mm}");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar disponibilidad[/]");
        var staffId = AnsiConsole.Ask<int>("Staff ID / Personal ID:");
        var availabilityStatusId = AnsiConsole.Ask<int>("Availability Status ID:");
        var startsAt = AnsiConsole.Ask<DateTime>("Starts At (yyyy-MM-dd HH:mm):");
        var endsAt = AnsiConsole.Ask<DateTime>("Ends At (yyyy-MM-dd HH:mm):");
        var notes = AskOptionalText("Notas (opcional):");

        if (AnsiConsole.Confirm("Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(staffId, availabilityStatusId, startsAt, endsAt, notes);
                AnsiConsole.MarkupLine("[green]Disponibilidad registrada exitosamente.[/]");
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            }
        }

        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] de la disponibilidad a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]Disponibilidad no encontrada.[/]");
            Pause();
            return;
        }

        var staffId = AnsiConsole.Ask<int>("Nuevo Staff ID / Personal ID:", item.StaffId.Value);
        var availabilityStatusId = AnsiConsole.Ask<int>("Nuevo Availability Status ID:", item.StateId.Value);
        var startsAt = AnsiConsole.Ask<DateTime>("Nuevo Starts At:", item.Dates.StartDate);
        var endsAt = AnsiConsole.Ask<DateTime>("Nuevo Ends At:", item.Dates.EndDate);
        var notes = AskOptionalText("Nuevas notas:", item.Observation.Value);

        try
        {
            await _service.UpdateAsync(id, staffId, availabilityStatusId, startsAt, endsAt, notes);
            AnsiConsole.MarkupLine("[green]Disponibilidad actualizada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeleteMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Menu de eliminacion[/]")
                .AddChoices("Eliminar por ID", "Eliminar por Staff ID", "Cancelar"));

        if (subOption == "Cancelar")
        {
            Pause();
            return;
        }

        try
        {
            switch (subOption)
            {
                case "Eliminar por ID":
                    var id = AnsiConsole.Ask<int>("ID a eliminar:");
                    if (AnsiConsole.Confirm("Esta seguro?"))
                        await _service.DeleteByIdAsync(id);
                    break;
                case "Eliminar por Staff ID":
                    var staffId = AnsiConsole.Ask<int>("Staff ID / Personal ID a eliminar:");
                    if (AnsiConsole.Confirm("Desea eliminar todas las disponibilidades de ese personal?"))
                    {
                        var affected = await _service.DeleteByStaffIdAsync(staffId);
                        AnsiConsole.MarkupLine($"[green]Registros eliminados: {affected}[/]");
                    }
                    break;
            }

            AnsiConsole.MarkupLine("[green]Operacion procesada.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowSearchResults(IEnumerable<StaffAvailabilityRecord> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<StaffAvailabilityRecord> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Staff ID[/]")
            .AddColumn("[blue]Status ID[/]")
            .AddColumn("[green]Starts At[/]")
            .AddColumn("[blue]Ends At[/]")
            .AddColumn("[green]Notes[/]");

        foreach (var item in items)
        {
            table.AddRow(
                (item.Id?.Value ?? 0).ToString(),
                item.StaffId.Value.ToString(),
                item.StateId.Value.ToString(),
                item.Dates.StartDate.ToString("yyyy-MM-dd HH:mm"),
                item.Dates.EndDate.ToString("yyyy-MM-dd HH:mm"),
                item.Observation.Value ?? "-");
        }

        AnsiConsole.Write(table);
    }

    private static string? AskOptionalText(string prompt, string? currentValue = null)
    {
        var text = AnsiConsole.Ask<string>(prompt, currentValue ?? string.Empty);
        return string.IsNullOrWhiteSpace(text) ? null : text.Trim();
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}