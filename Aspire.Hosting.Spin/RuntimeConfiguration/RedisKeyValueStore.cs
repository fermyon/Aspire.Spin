using System.Text;

namespace Aspire.Hosting;

public class RedisKeyValueStore : KeyValueStore, IEquatable<RedisKeyValueStore>
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

    public string Url { get; }

    public override string ToToml()
    {
        var builder = new StringBuilder(base.ToToml());
        builder.AppendLine($"url = \"{Url}\"");
        return builder.ToString();
    }
    
    public override int GetHashCode()
    {
        return Url?.GetHashCode() ?? 0;
    }

    public bool Equals(RedisKeyValueStore? other)
    {
        if (other == null)
        {
            return false;
        }
        return ReferenceEquals(this, other) || Url.Equals(other.Url);
    }

    public override bool Equals(object? obj) => Equals(obj as RedisKeyValueStore);
}