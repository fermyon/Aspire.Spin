using System.Text;

namespace Aspire.Hosting;

public class SqliteDatabase : ITomlize
{
    public SqliteDatabase(string path)
    {
        Path = path;
    }
    public string Path { get; private set; }

    public string ToToml()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"path = \"{Path}\"");
        return builder.ToString();
    }
}