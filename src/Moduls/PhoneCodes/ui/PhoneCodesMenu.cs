using GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.PhoneCodes.ui;

public sealed class PhoneCodesMenu : IModuleUI
{
    private readonly IPhoneCodesService _service;

    public string Key => "phone_codes";
    public string Title => "📞  Gestión de Códigos Telefónicos";

    public PhoneCodesMenu(IPhoneCodesService service)
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
                    .PageSize(10)
                    .AddChoices(new[]
                    {
                        "1. Listar todos los códigos telefónicos",
                        "2. Buscar código por ID",
                        "3. Buscar código por Country Code",
                        "4. Buscar código por Country Name",
                        "5. Registrar nuevo Código",
                        "6. Actualizar Código",
                        "7. Eliminar Código",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0")) break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByIdAsync(); break;
                case '3': await SearchByCountryCodeAsync(); break;
                case '4': await SearchByCountryNameAsync(); break;
                case '5': await CreateAsync(); break;
                case '6': await UpdateAsync(); break;
                case '7': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todos los Códigos Telefónicos");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del código telefónico:");
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún código telefónico con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByCountryCodeAsync()
    {
        var code = AnsiConsole.Ask<string>("Ingrese el [green]country code[/] (ej: +57):");
        var item = await _service.GetByCountryCodeAsync(code);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún código telefónico con ese country code.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para country code: {code}");

        Pause();
    }

    private async Task SearchByCountryNameAsync()
    {
        var countryName = AnsiConsole.Ask<string>("Ingrese el [green]country name[/]:");
        var item = await _service.GetByCountryNameAsync(countryName);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún código telefónico con ese country name.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para country name: {countryName}");

        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nuevo Código Telefónico[/]");
        var countryCode = AnsiConsole.Ask<string>("Country code:");
        var countryName = AnsiConsole.Ask<string>("Country name:");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(countryCode, countryName);
                AnsiConsole.MarkupLine("[green]✅ Código telefónico registrado exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] del código telefónico a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Código telefónico no encontrado.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"Modificando: [bold]{item.Code.Value}[/] - {item.CountryName.Value}");
        var newCountryCode = AnsiConsole.Ask<string>("Nuevo country code:", item.Code.Value);
        var newCountryName = AnsiConsole.Ask<string>("Nuevo country name:", item.CountryName.Value);

        try
        {
            await _service.UpdateAsync(id, newCountryCode, newCountryName);
            AnsiConsole.MarkupLine("[green]✅ Código telefónico actualizado.[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private async Task DeleteMenuAsync()
    {
        var subOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[red]Menú de Eliminación[/]")
                .AddChoices("Eliminar por ID", "Eliminar por Country Code", "Eliminar por Country Name", "Cancelar"));

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
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        await _service.DeleteByIdAsync(id);
                    break;
                case "Eliminar por Country Code":
                    var countryCode = AnsiConsole.Ask<string>("Country code a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        await _service.DeleteByCountryCodeAsync(countryCode);
                    break;
                case "Eliminar por Country Name":
                    var countryName = AnsiConsole.Ask<string>("Country name a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        await _service.DeleteByCountryNameAsync(countryName);
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

    private static void ShowTable(IEnumerable<PhoneCode> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Country Code[/]")
            .AddColumn("[blue]Country Name[/]");

        foreach (var item in items)
            table.AddRow(item.Id.Value.ToString(), item.Code.Value, item.CountryName.Value);

        AnsiConsole.Write(table);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
