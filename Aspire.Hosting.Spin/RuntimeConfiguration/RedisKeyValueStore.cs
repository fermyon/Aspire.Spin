using System.Text;

namespace Aspire.Hosting;

public class RedisKeyValueStore : KeyValueStore
{
    public RedisKeyValueStore(string url)
        : base("redis")
    {
        if (!url.StartsWith("redis://"))
        {
            url = $"redis://{url}";
        }
        Url = url;
    }

    public string Url { get; private set; }

    public override string ToToml()
    {
        var builder = new StringBuilder(base.ToToml());
        builder.AppendLine($"url = \"{Url}\"");
        return builder.ToString();
    }
}