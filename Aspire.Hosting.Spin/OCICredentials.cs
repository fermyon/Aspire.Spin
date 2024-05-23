namespace Aspire.Hosting;

public class OCICredentials(string loginServer, string user, string password)
{
    public static OCICredentials Create(string loginServer, string user, string password)
    {
        return new OCICredentials(loginServer, user,  password);
    }
    public string LoginServer { get; set; } = loginServer;
    public string User { get; set; } = user;
    public string Password { get; set; } = password;
}