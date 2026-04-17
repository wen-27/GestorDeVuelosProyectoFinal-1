using GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.Application.Interfaces;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.src.Moduls.AircraftModels.UI;

public sealed class AircraftModelsMenu : IModuleUI
{
    private readonly IAircraftModelsService _service;

    public AircraftModelsMenu(IAircraftModelsService service) => _service = service;

    public string Key   => "2";
    public string Title => "Modelos de Aeronave";

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(new Rule("[bold yellow]Modelos de Aeronave[/]"));

            var opcion = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Selecciona una opción:")
                    .AddChoices("Listar", "Crear", "Buscar por ID",
                                "Actualizar", "Eliminar", "Volver"));

            try
            {
                switch (opcion)
                {
                    case "Listar": await ListAsync(cancellationToken); 
                    break;
                    case "Crear": await CreateAsync(cancellationToken);  
                    break;
                    case "Buscar por ID": await GetByIdAsync(cancellationToken); break;
                    case "Actualizar": await 
                    UpdateAsync(cancellationToken);  break;
                    case "Eliminar": await 
                    DeleteAsync(cancellationToken);  break;
                    case "Volver": 
                    return;
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red]Error: {ex.Message}[/]");
                await Task.Delay(1500);
            }
        }
    }

    private async Task ListAsync(CancellationToken ct)
    {
        var models = await _service.GetAllAsync(ct);

        if (models.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No hay modelos registrados.[/]");
            Console.ReadKey();
            return;
        }

        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn("ID")
            .AddColumn("Nombre")
            .AddColumn("Capacidad")
            .AddColumn("Peso máx (kg)")
            .AddColumn("Combustible (kg/h)")
            .AddColumn("Velocidad (km/h)")
            .AddColumn("Altitud (ft)");

        foreach (var m in models)
            table.AddRow(
                m.Id.ToString(),
                m.ModelName.Value,
                m.MaxCapacity.Value.ToString(),
                m.MaxTakeoffWeight.ToString(),
                m.FuelConsumption.ToString(),
                m.CruiseSpeed.ToString(),
                m.CruiseAltitude.ToString());

        AnsiConsole.Write(table);
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Enter para continuar...[/]").AllowEmpty());
    }

    private async Task CreateAsync(CancellationToken ct)
    {
        AnsiConsole.Write(new Rule("[bold]Nuevo Modelo[/]"));

        var id = AnsiConsole.Prompt(new TextPrompt<int>("ID del modelo:"));
        var name = AnsiConsole.Prompt(new TextPrompt<string>("Nombre del modelo:"));
        var capacity = AnsiConsole.Prompt(new TextPrompt<int>("Capacidad máxima:"));
        var weight = AnsiConsole.Prompt(new TextPrompt<decimal?>("Peso máx despegue kg (Enter para omitir):").AllowEmpty());
        var fuel = AnsiConsole.Prompt(new TextPrompt<decimal?>("Consumo combustible kg/h (Enter para omitir):").AllowEmpty());
        var speed = AnsiConsole.Prompt(new TextPrompt<int?>("Velocidad crucero km/h (Enter para omitir):").AllowEmpty());
        var altitude = AnsiConsole.Prompt(new TextPrompt<int?>("Altitud crucero ft (Enter para omitir):").AllowEmpty());

        // ← ct en minúscula, sin "CancellationToken:"
        await _service.CreateAsync(id, name, capacity, weight, fuel, speed, altitude, ct);
        AnsiConsole.MarkupLine("[green]Modelo creado exitosamente.[/]");
        await Task.Delay(1500);
    }

    private async Task GetByIdAsync(CancellationToken ct)
    {
        var id = AnsiConsole.Prompt(new TextPrompt<int>("ID del modelo:"));
        var model = await _service.GetByIdAsync(id, ct);

        if (model is null)
        {
            AnsiConsole.MarkupLine("[red]Modelo no encontrado.[/]");
            await Task.Delay(1500);
            return;
        }

        AnsiConsole.MarkupLine($"[bold]ID:[/] {model.Id}");
        AnsiConsole.MarkupLine($"[bold]Nombre:[/] {model.ModelName.Value}");
        AnsiConsole.MarkupLine($"[bold]Capacidad:[/] {model.MaxCapacity.Value}");
        AnsiConsole.Prompt(new TextPrompt<string>("[grey]Enter para continuar...[/]").AllowEmpty());
    }

    private async Task UpdateAsync(CancellationToken ct)
    {
        var id = AnsiConsole.Prompt(new TextPrompt<int>("ID del modelo a actualizar:"));
        var model = await _service.GetByIdAsync(id, ct);

        if (model is null)
        {
            AnsiConsole.MarkupLine("[red]Modelo no encontrado.[/]");
            await Task.Delay(1500);
            return;
        }

        var name     = AnsiConsole.Prompt(new TextPrompt<string>("Nuevo nombre:").DefaultValue(model.ModelName.Value));
        var capacity = AnsiConsole.Prompt(new TextPrompt<int>("Nueva capacidad:").DefaultValue(model.MaxCapacity.Value));
        var weight   = AnsiConsole.Prompt(new TextPrompt<decimal?>("Nuevo peso máx kg (Enter omitir):").AllowEmpty());
        var fuel     = AnsiConsole.Prompt(new TextPrompt<decimal?>("Nuevo consumo kg/h (Enter omitir):").AllowEmpty());
        var speed    = AnsiConsole.Prompt(new TextPrompt<int?>("Nueva velocidad km/h (Enter omitir):").AllowEmpty());
        var altitude = AnsiConsole.Prompt(new TextPrompt<int?>("Nueva altitud ft (Enter omitir):").AllowEmpty());


        await _service.UpdateAsync(id, name, capacity, weight, fuel, speed, altitude, ct);
        AnsiConsole.MarkupLine("[green]Modelo actualizado.[/]");
        await Task.Delay(1500);
    }

    private async Task DeleteAsync(CancellationToken ct)
    {
        var id = AnsiConsole.Prompt(new TextPrompt<int>("ID del modelo a eliminar:"));

        var confirmar = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]¿Confirmar eliminación?[/]")
                .AddChoices("Sí, eliminar", "No, cancelar"));

        if (confirmar == "Sí, eliminar")
        {
            await _service.DeleteAsync(id, ct);
            AnsiConsole.MarkupLine("[green]Modelo eliminado.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[yellow]Operación cancelada.[/]");
        }

        await Task.Delay(1500);
    }
}