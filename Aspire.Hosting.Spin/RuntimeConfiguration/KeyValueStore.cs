using System.Text;

namespace Aspire.Hosting;

public abstract class KeyValueStore : ITomlize
{

    public KeyValueStore(string type)
    {
        Type = type;
    }

    public string Type { get; private set; }
    public virtual string ToToml()
    {
        var res = new StringBuilder();
        res.AppendLine($"type = \"{Type}\"");
        return res.ToString();
    }
}