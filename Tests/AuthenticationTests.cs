using SafeVaultDemo.Services;
using Xunit;

namespace SafeVaultDemo.Tests;

public class AuthenticationTests
{
    [Fact]
    public void Login_WithCorrectPassword_ShouldSucceed()
    {
        var service =
            new AuthenticationService();

        var result =
            service.Login(
                "admin",
                "Admin123!");

        Assert.NotNull(result);
    }

    [Fact]
    public void Login_WithWrongPassword_ShouldFail()
    {
        var service =
            new AuthenticationService();

        var result =
            service.Login(
                "admin",
                "WrongPassword");

        Assert.Null(result);
    }
}
