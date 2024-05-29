namespace Aspire.Hosting;

public class OciReference(string repository, string tag)
{
    public const string Latest = "latest";

    public static OciReference From(string repository, string tag)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(repository);
        return new OciReference(repository, tag);
    }
    
    public String Repository { get; } = repository;
    public String Tag { get; } = tag;

    public override string ToString()
    {
        return string.IsNullOrWhiteSpace(Tag) ? $"{Repository}:latest" : $"{Repository}:{Tag}";
    }
}