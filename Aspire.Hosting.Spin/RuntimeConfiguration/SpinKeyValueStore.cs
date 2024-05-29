using System.Text;

namespace Aspire.Hosting;

public class SpinKeyValueStore : KeyValueStore, IEquatable<SpinKeyValueStore>
{
    public SpinKeyValueStore(string path)
        : base("spin")
    {
        Path = path;
    }

    public string Path { get; }

    public override string ToToml()
    {
        var builder = new StringBuilder(base.ToToml());
        builder.AppendLine($"path = \"{Path}\"");
        return builder.ToString();
    }
    
    public override int GetHashCode()
    {
        return Path?.GetHashCode() ?? 0;
    }

    public bool Equals(SpinKeyValueStore? other)
    {
        if (other == null)
        {
            return false;
        }
        return ReferenceEquals(this, other) || Path.Equals(other.Path);
    }

    public override bool Equals(object? obj) => Equals(obj as SpinKeyValueStore);
}