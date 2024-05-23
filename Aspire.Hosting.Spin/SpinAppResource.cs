using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Utils;
namespace Aspire.Hosting;

public class SpinAppResource : ExecutableResource
{
    
    public SpinAppResource(string name, string workingDirectory)
        : this(name, Constants.SpinBinary, workingDirectory)
    {

    }

    internal SpinAppResource(string name, string command, string workingDirectory)
        : base(name, command, workingDirectory)
    {

    }

}
