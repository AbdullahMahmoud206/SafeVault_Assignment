using SafeVaultDemo.Services;
using Xunit;

namespace SafeVaultDemo.Tests;

public class SqlInjectionTests
{
    [Fact]
    public void SqlInjectionString_ShouldRemainData()
    {
        string injection =
            "' OR 1=1 --";

        string result =
            InputSanitizer.Sanitize(injection);

        Assert.NotNull(result);
    }

    [Fact]
    public void SqlInjectionInput_ShouldNotBeValidUsername()
    {
        string injection =
            "' OR 1=1 --";

        bool result =
            InputSanitizer.IsValidUsername(injection);

        Assert.False(result);
    }
}