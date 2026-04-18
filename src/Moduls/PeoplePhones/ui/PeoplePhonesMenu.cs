using GestorDeVuelosProyectoFinal.Moduls.PeoplePhones.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.PeoplePhones.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.PeoplePhones.ui;

public sealed class PeoplePhonesMenu : IModuleUI
{
    private readonly IPeoplePhonesService _service;

    public string Key => "people_phones";
    public string Title => "☎️  Gestión de Teléfonos de Personas";

    public PeoplePhonesMenu(IPeoplePhonesService service)
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
                    .PageSize(12)
                    .AddChoices(new[]
                    {
                        "1. Listar todos los teléfonos",
                        "2. Buscar por ID",
                        "3. Buscar por Person ID",
                        "4. Buscar por Phone Number",
                        "5. Buscar por Phone Code ID",
                        "6. Buscar por Nombre de Persona",
                        "7. Registrar nuevo Teléfono",
                        "8. Actualizar Teléfono",
                        "9. Eliminar Teléfono",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0")) break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByIdAsync(); break;
                case '3': await SearchByPersonIdAsync(); break;
                case '4': await SearchByPhoneNumberAsync(); break;
                case '5': await SearchByPhoneCodeIdAsync(); break;
                case '6': await SearchByPersonNameAsync(); break;
                case '7': await CreateAsync(); break;
                case '8': await UpdateAsync(); break;
                case '9': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var items = await _service.GetAllAsync();
        ShowTable(items, "Todos los Teléfonos de Personas");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] del teléfono:");
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ningún teléfono con ese ID.[/]");
        else
            ShowTable(new[] { item }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByPersonIdAsync()
    {
        var personId = AnsiConsole.Ask<int>("Ingrese el [green]Person ID[/]:");
        var items = await _service.GetByPersonIdAsync(personId);
        ShowSearchResults(items, $"Teléfonos de la persona #{personId}");
    }

    private async Task SearchByPhoneNumberAsync()
    {
        var phoneNumber = AnsiConsole.Ask<string>("Ingrese el [green]phone number[/]:");
        var items = await _service.GetByPhoneNumberAsync(phoneNumber);
        ShowSearchResults(items, $"Resultado para número: {phoneNumber}");
    }

    private async Task SearchByPhoneCodeIdAsync()
    {
        var phoneCodeId = AnsiConsole.Ask<int>("Ingrese el [green]Phone Code ID[/]:");
        var items = await _service.GetByPhoneCodeIdAsync(phoneCodeId);
        ShowSearchResults(items, $"Resultado para Phone Code ID: {phoneCodeId}");
    }

    private async Task SearchByPersonNameAsync()
    {
        var personName = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] de la persona:");
        var items = await _service.GetByPersonNameAsync(personName);
        ShowSearchResults(items, $"Resultado para nombre: {personName}");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nuevo Teléfono de Persona[/]");
        var personId = AnsiConsole.Ask<int>("Person ID:");
        var phoneCodeId = AnsiConsole.Ask<int>("Phone Code ID:");
        var phoneNumber = AnsiConsole.Ask<string>("Phone number:");
        var isPrimary = AnsiConsole.Confirm("¿Es teléfono principal?");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(personId, phoneCodeId, phoneNumber, isPrimary);
                AnsiConsole.MarkupLine("[green]✅ Teléfono registrado exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] del teléfono a modificar:");
        var item = await _service.GetByIdAsync(id);

        if (item is null)
        {
            AnsiConsole.MarkupLine("[red]❌ Teléfono no encontrado.[/]");
            Pause();
            return;
        }

        var personId = AnsiConsole.Ask<int>("Nuevo Person ID:", item.PersonId.Value);
        var phoneCodeId = AnsiConsole.Ask<int>("Nuevo Phone Code ID:", item.PhoneCodeId.Value);
        var phoneNumber = AnsiConsole.Ask<string>("Nuevo phone number:", item.PhoneNumber.Value);
        var isPrimary = AnsiConsole.Confirm("¿Es teléfono principal?", item.IsPrimary.Value);

        try
        {
            await _service.UpdateAsync(id, personId, phoneCodeId, phoneNumber, isPrimary);
            AnsiConsole.MarkupLine("[green]✅ Teléfono actualizado.[/]");
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
                .AddChoices("Eliminar por ID", "Eliminar por Phone Number", "Eliminar por Phone Code ID", "Eliminar por Nombre de Persona", "Cancelar"));

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
                case "Eliminar por Phone Number":
                    var phoneNumber = AnsiConsole.Ask<string>("Phone number a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros eliminados: {await _service.DeleteByPhoneNumberAsync(phoneNumber)}[/]");
                    break;
                case "Eliminar por Phone Code ID":
                    var phoneCodeId = AnsiConsole.Ask<int>("Phone Code ID a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros eliminados: {await _service.DeleteByPhoneCodeIdAsync(phoneCodeId)}[/]");
                    break;
                case "Eliminar por Nombre de Persona":
                    var personName = AnsiConsole.Ask<string>("Nombre de persona a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros eliminados: {await _service.DeleteByPersonNameAsync(personName)}[/]");
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

    private static void ShowSearchResults(IEnumerable<PersonPhone> items, string title)
    {
        var list = items.ToList();
        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]❌ No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<PersonPhone> items, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[green]Person ID[/]")
            .AddColumn("[blue]Phone Code ID[/]")
            .AddColumn("[green]Phone Number[/]")
            .AddColumn("[blue]Principal[/]");

        foreach (var item in items)
            table.AddRow(
                item.Id.Value.ToString(),
                item.PersonId.Value.ToString(),
                item.PhoneCodeId.Value.ToString(),
                item.PhoneNumber.Value,
                item.IsPrimary.Value ? "Sí" : "No");

        AnsiConsole.Write(table);
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
