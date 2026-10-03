using SafeVaultDemo.Models;
using SafeVault.Services;
using Xunit;

namespace SafeVault.Tests;

public class AuthorizationTests
{
    private readonly AuthorizationService _authorizationService;

    public AuthorizationTests()
    {
        _authorizationService =
            new AuthorizationService();
    }

    [Fact]
    public void Admin_Should_Access_Admin_Dashboard()
    {
        var admin = new User
        {
            Username = "admin",
            Role = "Admin"
        };

        var result =
            _authorizationService
                .CanAccessAdminDashboard(admin);

        Assert.True(result);
    }

    [Fact]
    public void User_Should_Not_Access_Admin_Dashboard()
    {
        var user = new User
        {
            Username = "user",
            Role = "User"
        };

        var result =
            _authorizationService
                .CanAccessAdminDashboard(user);

        Assert.False(result);
    }

    [Fact]
    public void Admin_Should_Access_User_Area()
    {
        var admin = new User
        {
            Username = "admin",
            Role = "Admin"
        };

        var result =
            _authorizationService
                .CanAccessUserArea(admin);

        Assert.True(result);
    }

    [Fact]
    public void User_Should_Access_User_Area()
    {
        var user = new User
        {
            Username = "user",
            Role = "User"
        };

        var result =
            _authorizationService
                .CanAccessUserArea(user);

        Assert.True(result);
    }

    [Fact]
    public void HasRole_Should_Return_True_For_Matching_Role()
    {
        var admin = new User
        {
            Role = "Admin"
        };

        var result =
            _authorizationService
                .HasRole(admin, "Admin");

        Assert.True(result);
    }

    [Fact]
    public void HasRole_Should_Return_False_For_NonMatching_Role()
    {
        var user = new User
        {
            Role = "User"
        };

        var result =
            _authorizationService
                .HasRole(user, "Admin");

        Assert.False(result);
    }
}