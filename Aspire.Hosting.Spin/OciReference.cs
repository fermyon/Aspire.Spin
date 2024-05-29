namespace Aspire.Hosting;

public class OciReference(string repository, string tag)
{
    public const string Latest = "latest";

    public string Repository { get; } = repository;
    public string Tag { get; } = tag;

    public static OciReference From(string repository, string tag)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(repository);
        return new OciReference(repository, tag);
    }

    public override string ToString()
    {
        return string.IsNullOrWhiteSpace(Tag) ? $"{Repository}:latest" : $"{Repository}:{Tag}";
    }
}