using FluentAssertions;

namespace Aspire.Hosting.Spin.Tests;

public class CheckForSpinTests
{
    [Fact]
    public async Task ItShouldPassIfSpinIsInstalled()
    {
        var sut = new CheckForSpin();
        var act = () => sut.BeforeStartAsync(null);

        await act.Should().NotThrowAsync();
    }

}