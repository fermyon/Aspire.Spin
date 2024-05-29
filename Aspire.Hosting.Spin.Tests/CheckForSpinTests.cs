using FluentAssertions;

namespace Aspire.Hosting.Spin.Tests;

public class CheckForSpinTests
{
    
    bool IsSpinInPath()
    {
        var paths = Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator);
        foreach (var path in paths)
        {
            var fullPath = Path.Combine(path, "spin");
            if (File.Exists(fullPath))
            {
                return true;
            }
        }
        return false;
    }

    [SkippableFact]
    public async Task ItShouldPassIfSpinIsInstalled()
    {
        Skip.IfNot(IsSpinInPath());
        var sut = new CheckForSpin();
        var act = () => sut.BeforeStartAsync(null);
        
        await act.Should().NotThrowAsync();
    }
    
    [SkippableFact]
    public async Task ItShouldFailIfSpinIsNotInstalled()
    {
        Skip.If(IsSpinInPath());
        var sut = new CheckForSpin();
        var act = () => sut.BeforeStartAsync(null);
        
        await act.Should().ThrowAsync<Exception>();
    }

}