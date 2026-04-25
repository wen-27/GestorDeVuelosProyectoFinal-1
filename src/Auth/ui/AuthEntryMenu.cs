using GestorDeVuelosProyectoFinal.src.Moduls.ClientPortal.UI;
using GestorDeVuelosProyectoFinal.src.Moduls.Customers.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.SystemRoles.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.src.Moduls.Users.Infrastructure.Entity;
using GestorDeVuelosProyectoFinal.Moduls.People.Infrastructure.Persistence.Entities;
using GestorDeVuelosProyectoFinal.src.Shared.Context;
using GestorDeVuelosProyectoFinal.src.Shared.Session;
using GestorDeVuelosProyectoFinal.src.Shared.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Spectre.Console;

namespace GestorDeVuelosProyectoFinal.Auth.ui;

// Este menú es la puerta de entrada del sistema.
// Desde aquí la persona inicia sesión, se registra y luego cae en el panel correcto.
public sealed class AuthEntryMenu
{
    private readonly LoginConsoleUI _login;
    private readonly IServiceScopeFactory _scopes;
    private readonly IConfiguration _configuration;

    public AuthEntryMenu(LoginConsoleUI login, IServiceScopeFactory scopes, IConfiguration configuration)
    {
        _login = login;
        _scopes = scopes;
        _configuration = configuration;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            AnsiConsole.Clear();
            RenderAccessHeader();

            // Leemos las credenciales demo desde configuración para no dejarlas fijas en código.
            var adminUser = _configuration["Auth:DefaultAdmin:Username"] ?? "admin";
            var adminPass = _configuration["Auth:DefaultAdmin:Password"] ?? "Admin123!";
            var demoUser = _configuration["Auth:DefaultUser:Username"] ?? "usuario";
            var demoPass = _configuration["Auth:DefaultUser:Password"] ?? "User123!";

            var credTable = new Table()
                .Border(TableBorder.Rounded)
                .BorderColor(Color.Grey)
                .AddColumn(new TableColumn("[bold grey]Rol[/]").Centered())
                .AddColumn("[bold grey]Usuario[/]")
                .AddColumn("[bold grey]Contraseña[/]");
            credTable.AddRow("[yellow]Administrador[/]", $"[white]{Markup.Escape(adminUser)}[/]", $"[dim]{Markup.Escape(adminPass)}[/]");
            credTable.AddRow("[deepskyblue1]Demo[/]", $"[white]{Markup.Escape(demoUser)}[/]", $"[dim]{Markup.Escape(demoPass)}[/]");

            AnsiConsole.Write(
                new Panel(credTable)
                    .Header("[bold grey]Cuentas de ejemplo[/]  [dim](Auth en appsettings.json)[/]")
                    .Border(BoxBorder.Square)
                    .BorderColor(Color.Grey));

            AnsiConsole.WriteLine();

            var option = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold white]¿Qué deseas hacer?[/]")
                    .HighlightStyle(new Style(foreground: Color.Cyan1, decoration: Decoration.Bold))
                    .AddChoices("Iniciar sesión", "Registrar usuario", "Salir"));

            switch (option)
            {
                case "Iniciar sesión":
                    await LoginFlowAsync(cancellationToken);
                    break;
                case "Registrar usuario":
                    await RegisterFlowAsync(cancellationToken);
                    break;
                case "Salir":
                    return;
            }
        }
    }

    private static void RenderAccessHeader()
    {
        AnsiConsole.Write(new FigletText("Acceso").Color(Color.Cyan1));
        AnsiConsole.Write(
            new Rule("[bold aqua]Gestor de Vuelos · identificación de usuario[/]")
                .RuleStyle(new Style(foreground: Color.DeepSkyBlue1))
                .LeftJustified());
        AnsiConsole.WriteLine();
    }

    private async Task LoginFlowAsync(CancellationToken cancellationToken)
    {
        var ok = await _login.ShowAsync();
        if (!ok)
            return;

        await PostLoginAsync(cancellationToken);
    }

    private async Task RegisterFlowAsync(CancellationToken cancellationToken)
    {
        AnsiConsole.Clear();
        RenderAccessHeader();

        AnsiConsole.Write(
            new Panel(new Markup("[grey]Crea una cuenta nueva. Se registrarán [bold]persona[/], [bold]usuario[/] y [bold]cliente[/] enlazados.[/]"))
                .Header("[bold white]Registrar usuario[/]")
                .Border(BoxBorder.Rounded)
                .BorderColor(Color.DeepSkyBlue1));

        AnsiConsole.WriteLine();

        using var scope = _scopes.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var documentTypeId = await PromptDocumentTypeIdAsync(db, cancellationToken);
        if (documentTypeId is null)
            return;

        var documentNumber = PromptNumericText("Número de documento", 1, 30);
        var firstName = PromptRequiredText("Nombres", 2, 80);
        var lastName = PromptRequiredText("Apellidos", 2, 80);

        var username = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Nuevo usuario[/] [grey]❯[/]")
                .PromptStyle(new Style(foreground: Color.White))
                .Validate(s => !string.IsNullOrWhiteSpace(s) && s.Trim().Length is >= 3 and <= 50
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Debe tener entre 3 y 50 caracteres.[/]")));

        var pass = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Contraseña[/] [grey]❯[/]")
                .PromptStyle(new Style(foreground: Color.White))
                .Secret()
                .Validate(s => !string.IsNullOrWhiteSpace(s) && s.Length >= 6
                    ? ValidationResult.Success()
                    : ValidationResult.Error("[red]Debe tener al menos 6 caracteres.[/]")));

        var pass2 = AnsiConsole.Prompt(
            new TextPrompt<string>("[deepskyblue1]Repetir contraseña[/] [grey]❯[/]")
                .PromptStyle(new Style(foreground: Color.White))
                .Secret());

        if (pass != pass2)
        {
            AnsiConsole.MarkupLine("\n[red]✗ Las contraseñas no coinciden.[/]");
            await Task.Delay(1200, cancellationToken);
            return;
        }

        try
        {
            var u = username.Trim();
            var exists = await db.Users.AsNoTracking().AnyAsync(x => x.Username == u, cancellationToken);
            if (exists)
            {
                await HandleExistingUsernameAsync(u, cancellationToken);
                return;
            }

            var person = await db.Persons.FirstOrDefaultAsync(
                x => x.DocumentTypeId == documentTypeId.Value && x.DocumentNumber == documentNumber,
                cancellationToken);

            if (person is not null)
            {
                var personAlreadyLinked = await db.Users.AsNoTracking()
                    .AnyAsync(x => x.Person_Id == person.Id, cancellationToken);

                if (personAlreadyLinked)
                    throw new InvalidOperationException("Ya existe un usuario registrado para esa persona/documento.");
            }

            var now = DateTime.UtcNow;
            person ??= new PersonEntity
            {
                DocumentTypeId = documentTypeId.Value,
                DocumentNumber = documentNumber,
                FirstName = firstName,
                LastName = lastName,
                BirthDate = null,
                Gender = null,
                AddressId = null,
                CreatedAt = now,
                UpdatedAt = now
            };

            if (person.Id == 0)
            {
                db.Persons.Add(person);
                await db.SaveChangesAsync(cancellationToken);
            }

            // Las cuentas nuevas entran como Client para caer directo al portal del usuario.
            var role = await db.SystemRoles.FirstOrDefaultAsync(r => r.Name == "Client", cancellationToken);
            if (role is null)
            {
                role = new SystemRolesEntity { Name = "Client", Description = "Cliente" };
                db.SystemRoles.Add(role);
                await db.SaveChangesAsync(cancellationToken);
            }

            db.Users.Add(new UsersEntity
            {
                Username = u,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(pass),
                Person_Id = person.Id,
                Role_Id = role.Id,
                IsActive = true,
                LastAccess = null,
                CreatedAt = now,
                UpdatedAt = now
            });

            var customerExists = await db.Customers.AsNoTracking()
                .AnyAsync(x => x.PersonId == person.Id, cancellationToken);

            if (!customerExists)
            {
                db.Customers.Add(new CustomerEntity
                {
                    PersonId = person.Id,
                    CreatedAt = now
                });
            }

            await db.SaveChangesAsync(cancellationToken);

            AnsiConsole.MarkupLine("\n[green]✓ Usuario registrado correctamente. Ya puedes iniciar sesión.[/]");
            await Task.Delay(1200, cancellationToken);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"\n[red]{Markup.Escape(ex.Message)}[/]");
            await Task.Delay(1500, cancellationToken);
        }
    }

    private async Task HandleExistingUsernameAsync(string username, CancellationToken cancellationToken)
    {
        AnsiConsole.MarkupLine($"\n[yellow]El usuario '{Markup.Escape(username)}' ya existe.[/]");

        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold white]¿Qué deseas hacer ahora?[/]")
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices("Iniciar sesión", "Registrar usuario", "Volver al menú principal"));

        switch (option)
        {
            case "Iniciar sesión":
                await LoginFlowAsync(cancellationToken);
                break;
            case "Registrar usuario":
                await RegisterFlowAsync(cancellationToken);
                break;
            default:
                break;
        }
    }

    private static string PromptRequiredText(string label, int min, int max)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"[deepskyblue1]{label}[/] [grey]❯[/]")
                .PromptStyle(new Style(foreground: Color.White))
                .Validate(s =>
                {
                    var value = s?.Trim() ?? string.Empty;
                    return value.Length >= min && value.Length <= max
                        ? ValidationResult.Success()
                        : ValidationResult.Error($"[red]Debe tener entre {min} y {max} caracteres.[/]");
                }))
            .Trim();
    }

    private static string PromptNumericText(string label, int min, int max)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"[deepskyblue1]{label}[/] [grey]❯[/]")
                .PromptStyle(new Style(foreground: Color.White))
                .Validate(s =>
                {
                    var value = s?.Trim() ?? string.Empty;
                    if (value.Length < min || value.Length > max)
                        return ValidationResult.Error($"[red]Debe tener entre {min} y {max} dígitos.[/]");

                    return value.All(char.IsDigit)
                        ? ValidationResult.Success()
                        : ValidationResult.Error("[red]Solo puede contener dígitos.[/]");
                }))
            .Trim();
    }

    private static async Task<int?> PromptDocumentTypeIdAsync(AppDbContext db, CancellationToken cancellationToken)
    {
        var docTypes = await db.DocumentTypes
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);

        if (docTypes.Count == 0)
            throw new InvalidOperationException("No hay tipos de documento registrados.");

        var options = docTypes
            .Select(x => $"{x.Code} · {x.Name}")
            .ToList();

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[deepskyblue1]Tipo de documento[/]")
                .PageSize(10)
                .HighlightStyle(new Style(foreground: Color.DeepSkyBlue1))
                .AddChoices(options));

        var index = options.IndexOf(selected);
        return index >= 0 ? docTypes[index].Id : null;
    }

    private async Task PostLoginAsync(CancellationToken cancellationToken)
    {
        var session = UserSession.Current ?? throw new InvalidOperationException("No hay una sesión activa.");
        var role = session.RoleName ?? "";

        using var scope = _scopes.CreateScope();
        // Según el rol enviamos a un menú distinto.
        if (string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase))
        {
            await scope.ServiceProvider.GetRequiredService<AdminNavigationMenu>().RunAsync(cancellationToken);
        }
        else if (string.Equals(role, "Client", StringComparison.OrdinalIgnoreCase)
                 || string.Equals(role, "User", StringComparison.OrdinalIgnoreCase))
        {
            await scope.ServiceProvider.GetRequiredService<ClientPortalMenu>().RunAsync(cancellationToken);
        }
        else
        {
            await scope.ServiceProvider.GetRequiredService<UserPortalPlaceholderMenu>().RunAsync(cancellationToken);
        }

        await _login.LogoutAsync();
    }
}
