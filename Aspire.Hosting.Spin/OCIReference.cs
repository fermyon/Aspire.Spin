namespace Aspire.Hosting;

public class OCIReference(string repository, string tag)
{
    public const string Latest = "latest";

    public static OCIReference From(string repository, string tag)
    {
        return new OCIReference(repository, tag);
    }
    
    public String Repository { get; } = repository;
    public String Tag { get; } = tag;

    public override string ToString()
    {
        return string.IsNullOrWhiteSpace(Tag) ? $"{Repository}:latest" : $"{Repository}:{Tag}";
    }
}