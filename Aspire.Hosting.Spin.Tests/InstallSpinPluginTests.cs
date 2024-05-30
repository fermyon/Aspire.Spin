using FluentAssertions;

namespace Aspire.Hosting.Spin.Tests;

public class InstallSpinPluginTests: LifecycleHookTests
{
    [SkippableFact]
    public async Task ItShouldInstallKubePluginIfSpinIsInstalled()
    {
        Skip.IfNot(IsSpinInPath());
        var sut = new InstallSpinPlugin("kube");
        var act = () => sut.BeforeStartAsync(null!);

        await act.Should().NotThrowAsync();
    }
    
    [SkippableFact]
    public async Task ItShouldFailIfSpinIsNotInstalled()
    {
        Skip.If(IsSpinInPath());
        var sut = new InstallSpinPlugin("kube");
        var act = () => sut.BeforeStartAsync(null!);

        await act.Should().ThrowAsync<Exception>();
    }
}