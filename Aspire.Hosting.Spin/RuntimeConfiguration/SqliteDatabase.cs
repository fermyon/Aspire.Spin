using System.Text;

namespace Aspire.Hosting;

public class SqliteDatabase : ITomlize, IEquatable<SqliteDatabase>
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


    public bool Equals(SqliteDatabase? other)
    {
        if (other == null)
        {
            return false;
        }
        return ReferenceEquals(this, other) || Path.Equals(other.Path);
    }

    public override bool Equals(object? obj) => Equals(obj as SqliteDatabase);
}