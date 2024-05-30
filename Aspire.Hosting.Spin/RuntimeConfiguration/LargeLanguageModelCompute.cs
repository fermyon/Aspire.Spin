using System.Text;

namespace Aspire.Hosting;

public class LargeLanguageModelCompute : ITomlize, IEquatable<LargeLanguageModelCompute>
{
    public LargeLanguageModelCompute(string url, string token)
    {
        Url = url;
        Token = token;
    }

    public string Url { get; }
    public string Token { get; }

    public bool Equals(LargeLanguageModelCompute? other)
    {
        if (other == null) return false;
        return ReferenceEquals(this, other) || (Url.Equals(other.Url) && Token.Equals(other.Token));
    }

    public string ToToml()
    {
        var builder = new StringBuilder();
        builder.AppendLine("type = \"remote_http\"");
        builder.AppendLine($"url = \"{Url}\"");
        builder.AppendLine($"auth_token = \"{Token}\"");
        return builder.ToString();
    }

    public override int GetHashCode()
    {
        const int prime1 = 17;
        const int prime2 = 31;
        var hash = prime1;
        hash = hash * prime2 + Url.GetHashCode();
        hash = hash * prime2 + Token.GetHashCode();
        return hash;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as LargeLanguageModelCompute);
    }
}