namespace Aspire.Hosting.Spin.Tests;

public abstract class LifecycleHookTests
{
    protected bool IsSpinInPath()
    {
        var paths = Environment.GetEnvironmentVariable("PATH")!.Split(Path.PathSeparator);
        return paths.Select(path => Path.Combine(path, "spin"))
            .Any(fullPath => File.Exists(fullPath));
    }
}