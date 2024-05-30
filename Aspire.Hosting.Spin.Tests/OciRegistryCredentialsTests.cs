using FluentAssertions;

namespace Aspire.Hosting.Spin.Tests;

public class OciRegistryCredentialsTests
{
    [Theory]
    [InlineData("some.registry.io", "", "")]
    [InlineData(null, "", "")]
    [InlineData("some.registry.io", "", null)]
    [InlineData("some.registry.io", null, "")]
    public void ItShouldNotAcceptNullOrEmptyValues(string loginServer, string user, string password)
    {
        var action = () => OciRegistryCredentials.Create(loginServer, user, password);
        action.Should().Throw<ArgumentException>();
    }
}