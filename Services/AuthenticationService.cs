using SafeVaultDemo.Models;
using SafeVaultDemo.Services;

namespace SafeVaultDemo.Services;

public class AuthenticationService
{
    private readonly List<User> _users;

    public AuthenticationService()
    {
        _users =
        [
            new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@safevault.com",
                PasswordHash =
                    PasswordService.HashPassword(
                        "Admin123!"),
                Role = "Admin"
            },

            new User
            {
                Id = 2,
                Username = "user",
                Email = "user@safevault.com",
                PasswordHash =
                    PasswordService.HashPassword(
                        "User123!"),
                Role = "User"
            }
        ];
    }

    public User? Login(
        string username,
        string password)
    {
        var user = _users.FirstOrDefault(
            x => x.Username == username);

        if (user is null)
            return null;

        return PasswordService.VerifyPassword(
            password,
            user.PasswordHash)
            ? user
            : null;
    }
}