using FluentAssertions;

namespace Aspire.Hosting.Spin.Tests;

public class SpinResourceTests
{
    [Fact]
    public void ItShouldSetCommandToSpin()
    {
        var sut = new SpinAppResource("foo", "./foo");
        sut.Command.Should().Be("spin");
    }

    [Fact]
    public void ItShouldSetTheWorkingDirOfTheSpinApp()
    {
        var sut = new SpinAppResource("foo", "./foo");
        sut.WorkingDirectory.Should().Be("./foo");
    }
}