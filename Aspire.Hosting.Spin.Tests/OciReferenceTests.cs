using FluentAssertions;

namespace Aspire.Hosting.Spin.Tests;

public class OciReferenceTests
{

    [Theory]
    [InlineData("nginx", "1-alpine", "nginx:1-alpine")]
    [InlineData("nginx", "1.0.0", "nginx:1.0.0")]
    [InlineData("nginx", "", "nginx:latest")]
    [InlineData("thorstenhans/hello", "0.0.1", "thorstenhans/hello:0.0.1")]
    
    public void ItShouldGenerateValidOciReferences(string repository, string tag, string expected)
    {
        var sut = OciReference.From(repository, tag);
        sut.ToString().Should().Be(expected);

    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    [InlineData(OciReference.Latest)]
    public void DefaultTagShouldBeLatest(string tag)
    {
        var sut = OciReference.From("hello-world", tag);
        sut.ToString().Should().Be("hello-world:latest");
    }


    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void OciReferenceShouldNotAllowNullOrEmptyForRepository(string repository)
    {
        var act = () => OciReference.From(repository, "some");
        act.Should().Throw<ArgumentException>();
    }
    
}