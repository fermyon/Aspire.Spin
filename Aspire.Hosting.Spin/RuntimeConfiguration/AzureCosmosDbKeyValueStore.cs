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

    public string Key { get; private set; }
    public string Account { get; private set; }
    public string Database { get; private set; }
    public string Container { get; private set; }

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