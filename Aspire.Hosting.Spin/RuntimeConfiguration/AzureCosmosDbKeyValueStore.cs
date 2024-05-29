using System.Text;

namespace Aspire.Hosting;

public class AzureCosmosDbKeyValueStore : KeyValueStore
{
    public AzureCosmosDbKeyValueStore(
        string key, string account, string database, string container
    )
        : base("azure_cosmos")
    {
        Key = key;
        Account = account;
        Database = database;
        Container = container;
    }

    public string Key { get; }
    public string Account { get; }
    public string Database { get; }
    public string Container { get; }

    public override string ToToml()
    {
        var builder = new StringBuilder(base.ToToml());
        builder.AppendLine($"key = \"{Key}\"");
        builder.AppendLine($"account = \"{Account}\"");
        builder.AppendLine($"database = \"{Database}\"");
        builder.AppendLine($"container = \"{Container}\"");
        return builder.ToString();
    }
}