using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Utils;
namespace Aspire.Hosting;

public enum SpinAppKind
{
    Http,
    StaticFileServer
}

public class SpinAppResource : ExecutableResource
{
    
    public SpinAppResource(string name, string workingDirectory)
        : this(name, Constants.SpinBinary, workingDirectory, SpinAppKind.Http)
    {

    }

    internal SpinAppResource(string name, string command, string workingDirectory, SpinAppKind kind)
        : base(name, command, workingDirectory)
    {
        Kind = kind;
    }
    public SpinAppKind Kind { get; private set; }


}
