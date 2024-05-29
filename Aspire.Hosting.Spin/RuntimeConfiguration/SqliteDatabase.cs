using System.Text;

namespace Aspire.Hosting;

public class SqliteDatabase : ITomlize, IEquatable<SqliteDatabase>
{
    public SqliteDatabase(string path)
    {
        Path = path;
    }

    public string Path { get; }

    public bool Equals(SqliteDatabase? other)
    {
        if (other == null) return false;
        return ReferenceEquals(this, other) || Path.Equals(other.Path);
    }

    public string ToToml()
    {
        var builder = new StringBuilder();
        builder.AppendLine($"path = \"{Path}\"");
        return builder.ToString();
    }

    public override int GetHashCode()
    {
        return Path?.GetHashCode() ?? 0;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as SqliteDatabase);
    }
}