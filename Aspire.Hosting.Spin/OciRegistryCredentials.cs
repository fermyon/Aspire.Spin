namespace Aspire.Hosting;

public class OciRegistryCredentials
{
    public static OciRegistryCredentials Create(string loginServer, string user, string password)
    {
        return new OciRegistryCredentials
        {
            LoginServer = loginServer,
            User = user,
            Password = password
        };
    }
    public string LoginServer { get; set; }
    public string User { get; set; }
    public string Password { get; set; } 
}