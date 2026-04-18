using Spectre.Console;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.Addresses.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;

namespace GestorDeVuelosProyectoFinal.Moduls.Addresses.UI;

public sealed class AddressMenu : IModuleUI
{
    private readonly IAddressService _service;

    public string Key => "addresses";
    public string Title => "🏠  Gestión de Direcciones";

    public AddressMenu(IAddressService service)
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
                    .AddChoices(new[] {
                        "1. Listar todas las direcciones",
                        "2. Buscar por Calle y Número",
                        "3. Registrar nueva Dirección",
                        "4. Actualizar Dirección",
                        "5. Eliminar Dirección",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0")) break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByStreetAsync(); break;
                case '3': await CreateAsync(); break;
                case '4': await UpdateAsync(); break;
                case '5': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var addresses = await _service.GetAllAsync();
        ShowTable(addresses, "Listado Global de Direcciones");
        Pause();
    }

    private async Task SearchByStreetAsync()
    {
        var street = AnsiConsole.Ask<string>("Ingrese el [green]nombre de la calle[/]:");
        var number = AnsiConsole.Prompt(new TextPrompt<string>("Ingrese el [green]número[/] (deje vacío si no tiene):").AllowEmpty());
        
        var address = await _service.GetByStreetAndNumberAsync(street, string.IsNullOrWhiteSpace(number) ? null : number);

        if (address == null)
        {
            AnsiConsole.MarkupLine("\n[red]❌ No se encontró ninguna dirección con esos datos.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("\n[green][bold]✓ Dirección Localizada[/][/]");
            ShowTable(new[] { address }, "Resultado de la búsqueda");
        }
        Pause();
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("\n[bold blue]--- REGISTRAR NUEVA DIRECCIÓN ---[/]");
        
        var streetTypeId = AnsiConsole.Ask<int>("ID del [yellow]Tipo de Vía[/] (ej. 1 para Calle):");
        var streetName = AnsiConsole.Ask<string>("Nombre de la [yellow]Vía[/]:");
        var number = AnsiConsole.Prompt(new TextPrompt<string>("Número (opcional):").AllowEmpty());
        var complement = AnsiConsole.Prompt(new TextPrompt<string>("Complemento/Apto (opcional):").AllowEmpty());
        var cityId = AnsiConsole.Ask<int>("ID de la [yellow]Ciudad[/]:");
        var postalCode = AnsiConsole.Prompt(new TextPrompt<string>("Código Postal (opcional):").AllowEmpty());

        if (AnsiConsole.Confirm("¿Desea guardar esta dirección?"))
        {
            await _service.CreateAsync(streetTypeId, streetName, number, complement, postalCode, cityId);
            AnsiConsole.MarkupLine("\n[green]✅ Dirección guardada exitosamente.[/]");
        }
        Pause();
    }

    private async Task UpdateAsync()
    {
        var id = AnsiConsole.Ask<int>("\nIngrese el [yellow]ID[/] de la dirección a modificar:");
        var address = await _service.GetByIdAsync(id);

        if (address == null)
        {
            AnsiConsole.MarkupLine("[red]❌ Dirección no encontrada.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"\nModificando: [bold cyan]{address.StreetName.Value} {address.Number.Value}[/]");
        
        var stId = AnsiConsole.Ask<int>("Nuevo ID Tipo Vía:", address.RoadTypeId.Value);
        var name = AnsiConsole.Ask<string>("Nuevo Nombre Calle:", address.StreetName.Value);
        var num = AnsiConsole.Ask<string>("Nuevo Número:", address.Number.Value ?? "");
        var comp = AnsiConsole.Ask<string>("Nuevo Complemento:", address.Complement.Value ?? "");
        var city = AnsiConsole.Ask<int>("Nuevo ID Ciudad:", address.CityId.Value);
        var pc = AnsiConsole.Ask<string>("Nuevo C.P.:", address.PostalCode.Value ?? "");

        await _service.UpdateAsync(id, stId, name, num, comp, pc, city);
        AnsiConsole.MarkupLine("\n[green]✅ Dirección actualizada correctamente.[/]");
        Pause();
    }

    private async Task DeleteMenuAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [red]ID[/] de la dirección a eliminar:");
        
        if (AnsiConsole.Confirm("[red]¿Está seguro de eliminar esta dirección? Esta acción es irreversible.[/]"))
        {
            var success = await _service.DeleteAsync(id);
            if (success) AnsiConsole.MarkupLine("\n[green]✅ Dirección eliminada.[/]");
            else AnsiConsole.MarkupLine("\n[red]❌ No se pudo eliminar (verifique si el ID existe).[/]");
        }
        Pause();
    }

    private void ShowTable(IEnumerable<Address> addresses, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .BorderColor(Color.Grey)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Tipo[/]")
            .AddColumn("[green]Calle/Vía[/]")
            .AddColumn("[green]Número[/]")
            .AddColumn("[green]Ciudad (ID)[/]")
            .AddColumn("[blue]C.P.[/]");

        foreach (var a in addresses)
        {
            table.AddRow(
                a.Id.Value.ToString(),
                a.RoadTypeId.Value.ToString(),
                a.StreetName.Value,
                a.Number.Value ?? "-",
                a.CityId.Value.ToString(),
                a.PostalCode.Value ?? "-"
            );
        }

        AnsiConsole.Write(table);
    }

    private void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}