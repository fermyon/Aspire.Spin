using Aspire.Hosting.ApplicationModel;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.Hosting;

public class SpinRuntimeConfigurationBuilder
{

    private IDictionary<string, IResourceBuilder<IResourceWithConnectionString>> _keyValueStores;
    private IDictionary<string, string> _sqliteDatabases;
    private string _name;

    private SpinRuntimeConfigurationBuilder()
    {
        _keyValueStores = new Dictionary<string, IResourceBuilder<IResourceWithConnectionString>>();
        _sqliteDatabases = new Dictionary<string, string>();
    }
    public static SpinRuntimeConfigurationBuilder Create(string fileName)
    {
        return new SpinRuntimeConfigurationBuilder()
        {
            _name = fileName
        };
    }
    
    public SpinRuntimeConfigurationBuilder WithRedisKeyValueStore(string name, IResourceBuilder<IResourceWithConnectionString> source)
    {
        _keyValueStores.Add(name, source);
        return this;
    }

    public SpinRuntimeConfigurationBuilder WithSqliteDatabase(string name, string path)
    {
        _sqliteDatabases.Add(name, path);
        return this;
    }


    public async Task<RuntimeConfiguration> Build()
    {
        var cfg = new RuntimeConfiguration(_name);
        foreach (var kv in _keyValueStores)
        {
            var url = await kv.Value.Resource.GetValueAsync();
            cfg.KeyValueStores.Add(kv.Key, new RedisKeyValueStore(url!));
        }

        foreach (var sqlite in _sqliteDatabases)
        {
            cfg.SqliteDatabases.Add(sqlite.Key, new SqliteDatabase(sqlite.Value));
        }
        return cfg;
    }
}
