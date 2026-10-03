using SafeVault.Services;
using SafeVaultDemo.Models;
using SafeVaultDemo.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddSingleton<AuthenticationService>();
builder.Services.AddSingleton<AuthorizationService>();

var app = builder.Build();

#region Activity 1 - Secure Registration

app.MapPost("/register",
    (string username, string email) =>
    {
        username = InputSanitizer.Sanitize(username);
        email = InputSanitizer.Sanitize(email);

        if (!InputSanitizer.IsValidUsername(username))
            return Results.BadRequest("Invalid username.");

        if (!InputSanitizer.IsValidEmail(email))
            return Results.BadRequest("Invalid email.");

        return Results.Ok(new
        {
            Message = "Registration successful",
            Username = username,
            Email = email
        });
    });

#endregion

#region Activity 2 - Authentication

app.MapPost("/login",
    (LoginRequest request,
     AuthenticationService authService) =>
    {
        var user = authService.Login(
            request.Username,
            request.Password);

        if (user is null)
            return Results.Unauthorized();

        return Results.Ok(new
        {
            Message = "Login Successful",
            user.Username,
            user.Role
        });
    });

#endregion

#region Activity 2 - Authorization

app.MapGet("/admin-dashboard",
(
string username,
string password,
AuthenticationService authService,
AuthorizationService authorizationService
) =>
{
    var user = authService.Login(
        username,
        password);

    if (user is null)
        return Results.Unauthorized();

    if (!authorizationService
        .CanAccessAdminDashboard(user))
    {
        return Results.Forbid();
    }

    return Results.Ok(new
    {
        Message = "Welcome Administrator"
    });
});


app.MapGet("/user-profile",
    (string username,
     string password,
     AuthenticationService authService) =>
    {
        var user = authService.Login(
            username,
            password);

        if (user is null)
            return Results.Unauthorized();

        return Results.Ok(new
        {
            user.Username,
            user.Role
        });
    });

#endregion

app.Run();