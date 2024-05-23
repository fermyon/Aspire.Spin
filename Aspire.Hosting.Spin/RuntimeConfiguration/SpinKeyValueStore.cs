using System.Text;

namespace Aspire.Hosting;

public class SpinKeyValueStore : KeyValueStore
{
    public SpinKeyValueStore(string path)
        : base("spin")
    {
        Path = path;
    }

    public string Path { get; private set; }

    public override string ToToml()
    {
        var builder = new StringBuilder(base.ToToml());
        builder.AppendLine($"path = \"{Path}\"");
        return builder.ToString();
    }
}