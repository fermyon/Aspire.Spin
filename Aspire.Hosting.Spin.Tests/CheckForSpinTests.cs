using FluentAssertions;

namespace Aspire.Hosting.Spin.Tests;

public class CheckForSpinTests: LifecycleHookTests
{
  

    [SkippableFact]
    public async Task ItShouldPassIfSpinIsInstalled()
    {
        Skip.IfNot(IsSpinInPath());
        var sut = new CheckForSpin();
        var act = () => sut.BeforeStartAsync(null!);

        await act.Should().NotThrowAsync();
    }

    [SkippableFact]
    public async Task ItShouldFailIfSpinIsNotInstalled()
    {
        Skip.If(IsSpinInPath());

        var sut = new CheckForSpin();
        var act = () => sut.BeforeStartAsync(null!);

        await act.Should().ThrowAsync<Exception>();
    }
}