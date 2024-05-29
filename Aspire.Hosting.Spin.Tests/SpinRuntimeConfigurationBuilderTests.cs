using Aspire.Hosting.ApplicationModel;
using FluentAssertions;
using Moq;

namespace Aspire.Hosting.Spin.Tests;

public class SpinRuntimeConfigurationBuilderTests
{
    [Fact]
    public async Task ItShouldProvideAFluentAPI()
    {
        var mockRedis = new Mock<IResourceBuilder<IResourceWithConnectionString>>();

        mockRedis.Setup(r => r.Resource.GetValueAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("redis://foo");
        
        var sut = await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
            .WithSqliteDatabase("default", "my.sqlite")
            .WithRedisKeyValueStore("foo", mockRedis.Object)
            .Build();

        sut.Should().NotBeNull();
    }

    [Fact]
    public async Task ItShouldSetSqliteDatabaseDetails()
    {
        var sut = await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
            .WithSqliteDatabase("default", "custom.sqlite")
            .Build();
        sut.SqliteDatabases.Should().HaveCount(1);
        sut.SqliteDatabases.Should().Contain("default", new SqliteDatabase("custom.sqlite"));
    }
    
    [Fact]
    public async Task ItShouldThrowWhenUserTriesToConfigureSqliteDatabaseTwiceWithSameName()
    {
        var action = async () =>
        {
            await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
                .WithSqliteDatabase("default", "custom.sqlite")
                .WithSqliteDatabase("default", "invalid.sqlite")
                .Build();
        };
        await action.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task ItShouldSetRedisKeyValueStoreDetails()
    {
        var redisMock = new Mock<IResourceBuilder<IResourceWithConnectionString>>();
        redisMock.Setup(r => r.Resource.GetValueAsync(It.IsAny<CancellationToken>())).ReturnsAsync("redis://some");
        var sut = await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
            .WithRedisKeyValueStore("some", redisMock.Object)
            .Build();

        sut.KeyValueStores.Should().HaveCount(1);
        sut.KeyValueStores.Should().Contain("some", new RedisKeyValueStore("redis://some"));
    }
    
    [Fact]
    public async Task ItShouldThrowWhenUserTriesToConfigureRedisKeyValueStoreTwiceWithSameName()
    {
        var redisMock = new Mock<IResourceBuilder<IResourceWithConnectionString>>();
        redisMock.Setup(r => r.Resource.GetValueAsync(It.IsAny<CancellationToken>())).ReturnsAsync("redis://some");
        var action = async () =>
        {
            await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
                .WithRedisKeyValueStore("default", redisMock.Object)
                .WithRedisKeyValueStore("default", redisMock.Object)
                .Build();
        };
        await action.Should().ThrowAsync<ArgumentException>();
    }
    
    [Fact]
    public async Task ItShouldThrowWhenUserTriesToConfigureSqliteKeyValueStoreTwiceWithSameName()
    {

        var action = async () =>
        {
            await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
                .WithSqliteKeyValueStore("default", "kv.sqlite")
                .WithSqliteKeyValueStore("default", "kv.sqlite")
                .Build();
        };
        await action.Should().ThrowAsync<ArgumentException>();
    }
    
    [Fact]
    public async Task ItShouldThrowWhenUserTriesToConfigureKeyValueStoresWithDuplicatedName()
    {
        var redisMock = new Mock<IResourceBuilder<IResourceWithConnectionString>>();
        redisMock.Setup(r => r.Resource.GetValueAsync(It.IsAny<CancellationToken>())).ReturnsAsync("redis://some");

        var action = async () =>
        {
            await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
                .WithSqliteKeyValueStore("default", "kv.sqlite")
                .WithRedisKeyValueStore("default", redisMock.Object)
                .Build();
        };
        await action.Should().ThrowAsync<ArgumentException>();
    }
    
    [Fact]
    public async Task ItShouldSetSqliteKeyValueStoreDetails()
    {
        var sut = await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
            .WithSqliteKeyValueStore("some", "kv.sqlite")
            .Build();

        sut.KeyValueStores.Should().HaveCount(1);
        sut.KeyValueStores.Should().Contain("some", new SpinKeyValueStore("kv.sqlite"));
    }
    
    [Fact]
    public async Task ItShouldSetMultipleKeyValueStoreDetails()
    {
        var redisMock = new Mock<IResourceBuilder<IResourceWithConnectionString>>();
        redisMock
            .Setup(r => r.Resource.GetValueAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync("redis://some");
        var sut = await SpinRuntimeConfigurationBuilder.Create("foo.yaml")
            .WithSqliteKeyValueStore("some", "kv.sqlite")
            .WithSqliteKeyValueStore("other", "kv2.sqlite")
            .WithRedisKeyValueStore("redis", redisMock.Object)
            .Build();

        sut.KeyValueStores.Should().HaveCount(3);
        sut.KeyValueStores.Should().Contain("some", new SpinKeyValueStore("kv.sqlite"));
        sut.KeyValueStores.Should().Contain("other", new SpinKeyValueStore("kv2.sqlite"));
        sut.KeyValueStores.Should().Contain("redis", new RedisKeyValueStore("redis://some"));
    }

    [Fact]
    public async Task ItShouldSetFileNameOnRuntimeConfiguration()
    {
        var name = "Some.yaml";
        var actual = await SpinRuntimeConfigurationBuilder.Create(name)
            .Build();
        actual.Name.Should().Be(name);
    }
}