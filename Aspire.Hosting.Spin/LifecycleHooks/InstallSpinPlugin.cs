using System.Diagnostics;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public class InstallSpinPlugin : Lifecycle.IDistributedApplicationLifecycleHook
{
    private readonly string _pluginName;
    public InstallSpinPlugin(string name)
    {
        _pluginName = name;
    }
    
    public async Task BeforeStartAsync(DistributedApplicationModel appModel,
        CancellationToken cancellationToken = new())
    {
        try
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = Constants.SpinBinary,
                    Arguments = $"{Constants.SpinCommands.Plugins} {Constants.SpinCommands.Install} {_pluginName} {Constants.SpinFlags.Confirm}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                }
            };

            process.Start();

            await process.StandardOutput.ReadToEndAsync();
            await process.StandardError.ReadToEndAsync();

            await process.WaitForExitAsync();
        }
        catch (Exception)
        {
            throw new Exception($"Installing Spin Plugin {_pluginName} failed");
        }
    }
}