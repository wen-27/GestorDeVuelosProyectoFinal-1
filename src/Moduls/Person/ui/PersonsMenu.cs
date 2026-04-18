using GestorDeVuelosProyectoFinal.Moduls.People.Application.Interfaces;
using GestorDeVuelosProyectoFinal.Moduls.People.Domain.Aggregate;
using GestorDeVuelosProyectoFinal.src.Shared.ui;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Moduls.People.ui;

public sealed class PersonsMenu : IModuleUI
{
    private readonly IPersonService _service;

    public string Key => "persons";
    public string Title => "👤  Gestión de Personas";

    public PersonsMenu(IPersonService service)
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
                        "1. Listar todas las personas",
                        "2. Buscar persona por ID",
                        "3. Buscar personas por Nombre",
                        "4. Buscar personas por Apellido",
                        "5. Buscar personas por Número de Documento",
                        "6. Registrar nueva Persona",
                        "7. Actualizar Persona",
                        "8. Eliminar Persona",
                        "0. Volver al menú principal"
                    }));

            if (option.StartsWith("0")) break;

            switch (option[0])
            {
                case '1': await ListAllAsync(); break;
                case '2': await SearchByIdAsync(); break;
                case '3': await SearchByFirstNameAsync(); break;
                case '4': await SearchByLastNameAsync(); break;
                case '5': await SearchByDocumentNumberAsync(); break;
                case '6': await CreateAsync(); break;
                case '7': await UpdateAsync(); break;
                case '8': await DeleteMenuAsync(); break;
            }
        }
    }

    private async Task ListAllAsync()
    {
        var people = await _service.GetAllAsync();
        ShowTable(people, "Todas las Personas");
        Pause();
    }

    private async Task SearchByIdAsync()
    {
        var id = AnsiConsole.Ask<int>("Ingrese el [green]ID[/] de la persona:");
        var person = await _service.GetByIdAsync(id);

        if (person == null)
            AnsiConsole.MarkupLine("[red]❌ No se encontró ninguna persona con ese ID.[/]");
        else
            ShowTable(new[] { person }, $"Resultado para ID: {id}");

        Pause();
    }

    private async Task SearchByFirstNameAsync()
    {
        var firstName = AnsiConsole.Ask<string>("Ingrese el [green]nombre[/] de la persona:");
        var people = await _service.GetByFirstNameAsync(firstName);
        ShowSearchResults(people, $"Resultado para nombre: {firstName}");
    }

    private async Task SearchByLastNameAsync()
    {
        var lastName = AnsiConsole.Ask<string>("Ingrese el [green]apellido[/] de la persona:");
        var people = await _service.GetByLastNameAsync(lastName);
        ShowSearchResults(people, $"Resultado para apellido: {lastName}");
    }

    private async Task SearchByDocumentNumberAsync()
    {
        var documentNumber = AnsiConsole.Ask<string>("Ingrese el [green]número de documento[/]:");
        var people = await _service.GetByDocumentNumberAsync(documentNumber);
        ShowSearchResults(people, $"Resultado para documento: {documentNumber}");
    }

    private async Task CreateAsync()
    {
        AnsiConsole.MarkupLine("[bold blue]Registrar Nueva Persona[/]");
        var documentTypeId = AnsiConsole.Ask<int>("ID del tipo de documento:");
        var documentNumber = AnsiConsole.Ask<string>("Número de documento:");
        var firstName = AnsiConsole.Ask<string>("Nombres:");
        var lastName = AnsiConsole.Ask<string>("Apellidos:");
        var birthDate = AskOptionalDate("Fecha de nacimiento (opcional, ENTER para omitir):");
        var gender = AskOptionalGender();
        var addressId = AskOptionalInt("ID de dirección (opcional, ENTER para omitir):");

        if (AnsiConsole.Confirm("¿Desea guardar los cambios?"))
        {
            try
            {
                await _service.CreateAsync(documentTypeId, documentNumber, firstName, lastName, birthDate, gender, addressId);
                AnsiConsole.MarkupLine("[green]✅ Persona registrada exitosamente.[/]");
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
        var id = AnsiConsole.Ask<int>("Ingrese el [yellow]ID[/] de la persona a modificar:");
        var person = await _service.GetByIdAsync(id);

        if (person == null)
        {
            AnsiConsole.MarkupLine("[red]❌ Persona no encontrada.[/]");
            Pause();
            return;
        }

        AnsiConsole.MarkupLine($"Modificando: [bold]{person.FirstName.Value} {person.LastNames.Value}[/]");
        var documentTypeId = AnsiConsole.Ask<int>("Nuevo ID de tipo de documento:", person.DocumentTypeId.Value);
        var documentNumber = AnsiConsole.Ask<string>("Nuevo número de documento:", person.DocumentNumber.Value);
        var firstName = AnsiConsole.Ask<string>("Nuevos nombres:", person.FirstName.Value);
        var lastName = AnsiConsole.Ask<string>("Nuevos apellidos:", person.LastNames.Value);
        var birthDate = AskOptionalDate("Nueva fecha de nacimiento (opcional, ENTER para omitir):", person.BirthDate.Value);
        var gender = AskOptionalGender(person.Gender.Value);
        var addressId = AskOptionalInt("Nuevo ID de dirección (opcional, ENTER para omitir):", person.AddressId?.Value);

        try
        {
            await _service.UpdateAsync(id, documentTypeId, documentNumber, firstName, lastName, birthDate, gender, addressId);
            AnsiConsole.MarkupLine("[green]✅ Persona actualizada.[/]");
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
                .AddChoices("Eliminar por ID", "Eliminar por Nombre", "Eliminar por Apellido", "Eliminar por Documento", "Cancelar"));

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
                case "Eliminar por Nombre":
                    var firstName = AnsiConsole.Ask<string>("Nombre a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros eliminados: {await _service.DeleteByFirstNameAsync(firstName)}[/]");
                    break;
                case "Eliminar por Apellido":
                    var lastName = AnsiConsole.Ask<string>("Apellido a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros eliminados: {await _service.DeleteByLastNameAsync(lastName)}[/]");
                    break;
                case "Eliminar por Documento":
                    var documentNumber = AnsiConsole.Ask<string>("Documento a eliminar:");
                    if (AnsiConsole.Confirm("[red]¿Está seguro? Esta acción es irreversible.[/]"))
                        AnsiConsole.MarkupLine($"[green]✅ Registros eliminados: {await _service.DeleteByDocumentNumberAsync(documentNumber)}[/]");
                    break;
            }
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]❌ {ex.Message}[/]");
        }

        Pause();
    }

    private static void ShowSearchResults(IEnumerable<Person> people, string title)
    {
        var list = people.ToList();

        if (list.Count == 0)
            AnsiConsole.MarkupLine("[red]❌ No se encontraron registros.[/]");
        else
            ShowTable(list, title);

        Pause();
    }

    private static void ShowTable(IEnumerable<Person> people, string title)
    {
        var table = new Table()
            .Title(title)
            .Border(TableBorder.Rounded)
            .AddColumn("[yellow]ID[/]")
            .AddColumn("[blue]Tipo Doc[/]")
            .AddColumn("[green]Documento[/]")
            .AddColumn("[green]Nombres[/]")
            .AddColumn("[green]Apellidos[/]")
            .AddColumn("[blue]Nacimiento[/]")
            .AddColumn("[blue]Género[/]")
            .AddColumn("[blue]Dirección[/]");

        foreach (var person in people)
        {
            table.AddRow(
                person.Id.Value.ToString(),
                person.DocumentTypeId.Value.ToString(),
                person.DocumentNumber.Value,
                person.FirstName.Value,
                person.LastNames.Value,
                person.BirthDate.Value?.ToString("yyyy-MM-dd") ?? "-",
                person.Gender.Value?.ToString() ?? "-",
                person.AddressId?.Value.ToString() ?? "-"
            );
        }

        AnsiConsole.Write(table);
    }

    private static DateTime? AskOptionalDate(string prompt, DateTime? currentValue = null)
    {
        var raw = AnsiConsole.Ask<string>(prompt, currentValue?.ToString("yyyy-MM-dd") ?? string.Empty);
        if (string.IsNullOrWhiteSpace(raw)) return null;
        return DateTime.Parse(raw);
    }

    private static int? AskOptionalInt(string prompt, int? currentValue = null)
    {
        var raw = AnsiConsole.Ask<string>(prompt, currentValue?.ToString() ?? string.Empty);
        if (string.IsNullOrWhiteSpace(raw)) return null;
        return int.Parse(raw);
    }

    private static char? AskOptionalGender(char? currentValue = null)
    {
        var raw = AnsiConsole.Ask<string>("Género (M/F/N, opcional):", currentValue?.ToString() ?? string.Empty);
        if (string.IsNullOrWhiteSpace(raw)) return null;
        return raw.Trim()[0];
    }

    private static void Pause()
    {
        AnsiConsole.MarkupLine("\nPresione [grey]ENTER[/] para continuar...");
        Console.ReadLine();
    }
}
