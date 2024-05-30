using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public class SpinRuntimeConfigurationBuilder
{
    private readonly IDictionary<string, IResourceBuilder<IResourceWithConnectionString>> _aspireKeyValueStores;
    private readonly IDictionary<string, string> _keyValueStores;
    private string _name = null!;
    private readonly IDictionary<string, string> _sqliteDatabases;
    private string _llmUrl;
    private string _llmToken;

    private SpinRuntimeConfigurationBuilder()
    {
        _aspireKeyValueStores = new Dictionary<string, IResourceBuilder<IResourceWithConnectionString>>();
        _keyValueStores = new Dictionary<string, string>();
        _sqliteDatabases = new Dictionary<string, string>();
    }

    public static SpinRuntimeConfigurationBuilder Create(string fileName)
    {
        return new SpinRuntimeConfigurationBuilder
        {
            _name = fileName
        };
    }

    public SpinRuntimeConfigurationBuilder WithRedisKeyValueStore(string name,
        IResourceBuilder<IResourceWithConnectionString> source)
    {
        if (_keyValueStores.ContainsKey(name) || !_aspireKeyValueStores.TryAdd(name, source))
            throw new ArgumentException($"Key-Value Store {name} already configured");
        return this;
    }

    public SpinRuntimeConfigurationBuilder WithSqliteKeyValueStore(string name, string path)
    {
        if (_aspireKeyValueStores.ContainsKey(name) || !_keyValueStores.TryAdd(name, path))
            throw new ArgumentException($"Key-Value Store {name} already configured");

        return this;
    }

    public SpinRuntimeConfigurationBuilder WithSqliteDatabase(string name, string path)
    {
        if (!_sqliteDatabases.TryAdd(name, path))
            throw new ArgumentException($"SqliteDatabase {name} already configured");
        return this;
    }

    public SpinRuntimeConfigurationBuilder WithLLM(string url, string token)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(url);
        ArgumentException.ThrowIfNullOrWhiteSpace(token);
        _llmUrl = url;
        _llmToken = token;
        return this;
    }

    public async Task<RuntimeConfiguration> Build()
    {
        var cfg = new RuntimeConfiguration(_name);
        foreach (var kv in _aspireKeyValueStores)
        {
            var url = await kv.Value.Resource.GetValueAsync();
            cfg.KeyValueStores.Add(kv.Key, new RedisKeyValueStore(url!));
        }

        foreach (var kv in _keyValueStores) cfg.KeyValueStores.Add(kv.Key, new SpinKeyValueStore(kv.Value));

        foreach (var sqlite in _sqliteDatabases) cfg.SqliteDatabases.Add(sqlite.Key, new SqliteDatabase(sqlite.Value));
        
        if (!string.IsNullOrWhiteSpace(_llmUrl))
        {
            cfg.LLMCompute = new LargeLanguageModelCompute(_llmUrl, _llmToken);
        }
        return cfg;
    }
}