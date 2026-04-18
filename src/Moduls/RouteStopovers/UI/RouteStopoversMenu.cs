// Consola interactiva: Spectre.Console (https://spectreconsole.net/)
using Spectre.Console;
using GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.src.Moduls.RouteStopovers.UI;

public sealed class RouteStopoversMenu : IModuleUI
{
    private readonly IRouteStopoversService _service;

    public RouteStopoversMenu(IRouteStopoversService service)
    {
        _service = service;
    }

    public string Key => "route-stopovers";
    public string Title => "Escalas de ruta";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold deepskyblue1] Escalas de ruta [/]").LeftJustified());

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n[grey]Usa las flechas para navegar[/]")
                    .PageSize(10)
                    .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                    .AddChoices(
                        "Listar todas",
                        "Buscar por ID",
                        "Buscar por ruta",
                        "Crear escala",
                        "Actualizar escala",
                        "Eliminar por ID",
                        "Eliminar por ruta",
                        "Volver"));

            switch (option)
            {
                case "Listar todas":
                    await ListAllAsync(cancellationToken);
                    break;
                case "Buscar por ID":
                    await SearchByIdAsync(cancellationToken);
                    break;
                case "Buscar por ruta":
                    await SearchByRouteAsync(cancellationToken);
                    break;
                case "Crear escala":
                    await CreateAsync(cancellationToken);
                    break;
                case "Actualizar escala":
                    await UpdateAsync(cancellationToken);
                    break;
                case "Eliminar por ID":
                    await DeleteByIdAsync(cancellationToken);
                    break;
                case "Eliminar por ruta":
                    await DeleteByRouteAsync(cancellationToken);
                    break;
                case "Volver":
                    return;
            }
        }
    }

    private async Task ListAllAsync(CancellationToken cancellationToken)
    {
        RenderTable(await _service.GetAllAsync(cancellationToken), "Todas las escalas");
        Pause();
    }

    private async Task SearchByIdAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID de la escala:");
        var item = await _service.GetByIdAsync(id, cancellationToken);
        RenderTable(item is null ? Array.Empty<RouteStopover>() : new[] { item }, $"Resultado para ID {id}");
        Pause();
    }

    private async Task SearchByRouteAsync(CancellationToken cancellationToken)
    {
        var routeId = PromptPositiveInt("ID de la ruta:");
        RenderTable(await _service.GetByRouteIdAsync(routeId, cancellationToken), $"Escalas de la ruta {routeId}");
        Pause();
    }

    private async Task CreateAsync(CancellationToken cancellationToken)
    {
        var routeId = PromptPositiveInt("ID de la ruta:");
        var stopoverAirportId = PromptPositiveInt("ID aeropuerto de escala (stopover_airport_id):");
        var stopOrder = PromptPositiveInt("Orden de escala (stop_order):");
        var layoverMin = PromptNonNegativeInt("Layover en minutos (layover_min):");

        try
        {
            await _service.CreateAsync(routeId, stopoverAirportId, stopOrder, layoverMin, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Escala creada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task UpdateAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID de la escala a actualizar:");
        var current = await _service.GetByIdAsync(id, cancellationToken);
        if (current is null)
        {
            AnsiConsole.MarkupLine("\n[yellow]No se encontró la escala.[/]");
            Pause();
            return;
        }

        var routeId = PromptPositiveInt("ID de la ruta:", current.RouteId.Value);
        var stopoverAirportId = PromptPositiveInt("ID aeropuerto de escala:", current.StopoverAirportId.Value);
        var stopOrder = PromptPositiveInt("stop_order:", current.StopOrder.Value);
        var layoverMin = PromptNonNegativeInt("layover_min:", current.LayoverMin.Value);

        try
        {
            await _service.UpdateAsync(id, routeId, stopoverAirportId, stopOrder, layoverMin, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Escala actualizada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task DeleteByIdAsync(CancellationToken cancellationToken)
    {
        var id = PromptPositiveInt("ID de la escala a eliminar:");
        if (!AnsiConsole.Confirm("¿Confirmas la eliminación?", false))
        {
            Pause();
            return;
        }

        try
        {
            await _service.DeleteByIdAsync(id, cancellationToken);
            AnsiConsole.MarkupLine("\n[green]Escala eliminada correctamente.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private async Task DeleteByRouteAsync(CancellationToken cancellationToken)
    {
        var routeId = PromptPositiveInt("ID de la ruta (se eliminan todas sus escalas):");
        if (!AnsiConsole.Confirm("¿Confirmas la eliminación?", false))
        {
            Pause();
            return;
        }

        try
        {
            var n = await _service.DeleteByRouteIdAsync(routeId, cancellationToken);
            AnsiConsole.MarkupLine($"\n[green]Eliminadas {n} escala(s).[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
        }

        Pause();
    }

    private static void RenderTable(IEnumerable<RouteStopover> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
        {
            AnsiConsole.MarkupLine("\n[yellow]No hay registros para mostrar.[/]");
            return;
        }

        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[bold grey]ID[/]")
            .AddColumn("[bold grey]route_id[/]")
            .AddColumn("[bold grey]stopover_airport_id[/]")
            .AddColumn("[bold grey]stop_order[/]")
            .AddColumn("[bold grey]layover_min[/]");

        foreach (var item in list)
        {
            table.AddRow(
                item.Id?.Value.ToString() ?? "-",
                item.RouteId.Value.ToString(),
                item.StopoverAirportId.Value.ToString(),
                item.StopOrder.Value.ToString(),
                item.LayoverMin.Value.ToString());
        }

        AnsiConsole.Write(table);
    }

    private static int PromptPositiveInt(string label, int? current = null)
        => AnsiConsole.Prompt(new TextPrompt<int>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? 1)
            .Validate(v => v > 0 ? ValidationResult.Success() : ValidationResult.Error("[red]Debe ser mayor que cero.[/]")));

    private static int PromptNonNegativeInt(string label, int? current = null)
        => AnsiConsole.Prompt(new TextPrompt<int>($"[deepskyblue1]{label}[/]")
            .DefaultValue(current ?? 0)
            .Validate(v => v >= 0 ? ValidationResult.Success() : ValidationResult.Error("[red]No puede ser negativo.[/]")));

    private static void Pause()
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Presiona Enter para continuar...[/]").AllowEmpty());
    }
}
