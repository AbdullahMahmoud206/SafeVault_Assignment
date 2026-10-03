using SafeVaultDemo.Services;
using Xunit;

namespace SafeVaultDemo.Tests;

public class InputValidationTests
{
    [Fact]
    public void XssScript_ShouldBeRemoved()
    {
        string maliciousInput =
            "<script>alert('Hacked')</script>";

        string result =
            InputSanitizer.Sanitize(maliciousInput);

        Assert.DoesNotContain(
            "<script>",
            result,
            StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Username_ShouldBeValidated()
    {
        bool result =
            InputSanitizer.IsValidUsername("abdullah_123");

        Assert.True(result);
    }

    [Fact]
    public void InvalidUsername_ShouldFail()
    {
        bool result =
            InputSanitizer.IsValidUsername("<script>");

        Assert.False(result);
    }
}