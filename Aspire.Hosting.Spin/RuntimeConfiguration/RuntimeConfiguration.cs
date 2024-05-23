using System.Runtime.CompilerServices;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace Aspire.Hosting;

// hack: simple dump of runtime configuration
public class RuntimeConfiguration : ITomlize
{
    internal RuntimeConfiguration()
    {
        KeyValueStores = new Dictionary<string, KeyValueStore>();
        SqliteDatabases = new Dictionary<string, SqliteDatabase>();
    }

    // todo: consider hiding the dictionaries and expose only necessary APIs
    public IDictionary<string, KeyValueStore> KeyValueStores { get; set; }
    public IDictionary<string, SqliteDatabase> SqliteDatabases { get; set; }

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
            {
                builder.AppendLine($"[sqlite_database.{sqlite.Key}]");
                builder.AppendLine(sqlite.Value.ToToml());
            }
        }

        return builder.ToString();
    }
}