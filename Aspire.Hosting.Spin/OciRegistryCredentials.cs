namespace Aspire.Hosting;

public class OciRegistryCredentials
{
    public required string LoginServer { get; init; }
    public required string User { get; init; }
    public required string Password { get; init; }

    public static OciRegistryCredentials Create(string loginServer, string user, string password)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(loginServer);
        ArgumentException.ThrowIfNullOrWhiteSpace(user);
        ArgumentException.ThrowIfNullOrWhiteSpace(password);
        return new OciRegistryCredentials
        {
            LoginServer = loginServer,
            User = user,
            Password = password
        };
    }
}