using System.Text;

namespace Aspire.Hosting;

// hack: simple dump of runtime configuration
public class RuntimeConfiguration : ITomlize
{
    internal RuntimeConfiguration(string name)
    {
        Name = name;
        KeyValueStores = new Dictionary<string, KeyValueStore>();
        SqliteDatabases = new Dictionary<string, SqliteDatabase>();
        LLMCompute = null;
    }

    // todo: consider hiding the dictionaries and expose only necessary APIs
    public IDictionary<string, KeyValueStore> KeyValueStores { get; set; }
    public IDictionary<string, SqliteDatabase> SqliteDatabases { get; set; }
    public LargeLanguageModelCompute? LLMCompute { get; set; }
    public string Name { get; private set; }

    public string ToToml()
    {
        var builder = new StringBuilder();

        foreach (var kv in KeyValueStores)
        {
            builder.AppendLine($"[key_value_store.{kv.Key}]");
            builder.AppendLine(kv.Value.ToToml());
        }

        foreach (var sqlite in SqliteDatabases)
        {
            builder.AppendLine($"[sqlite_database.{sqlite.Key}]");
            builder.AppendLine(sqlite.Value.ToToml());
        }

        if (LLMCompute != null)
        {
            builder.AppendLine("[llm_compute]");
            builder.AppendLine(LLMCompute.ToToml());
        }

        return builder.ToString();
    }
}