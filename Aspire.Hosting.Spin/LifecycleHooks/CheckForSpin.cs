using System.Diagnostics;
using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public class CheckForSpin : Lifecycle.IDistributedApplicationLifecycleHook
{
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
                    Arguments = Constants.SpinFlags.Version,
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
            throw new Exception("Spin CLI is not installed");
        }
    }
}