using System.Diagnostics;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public class SpinRegistryLogin: Lifecycle.IDistributedApplicationLifecycleHook {
    private readonly OCICredentials _creds;

    public SpinRegistryLogin(OCICredentials creds)
    {
        _creds = creds;
    }
    
    public async Task BeforeStartAsync(DistributedApplicationModel appModel,
        CancellationToken cancellationToken = new())
    {
        try
        {
            var login = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Constants.SpinBinary,
                    Arguments = $"{Constants.SpinCommands.Registry} {Constants.SpinCommands.Login} {_creds.LoginServer} -u {_creds.User} -p {_creds.Password}",
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                }
            };
            login.Start();
            await login.StandardOutput.ReadToEndAsync();
            await login.StandardError.ReadToEndAsync();

            await login.WaitForExitAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while authenticating against OCI registry {_creds.LoginServer}", ex);
        }
    }
}