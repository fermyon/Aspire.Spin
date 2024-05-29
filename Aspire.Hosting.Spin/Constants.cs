namespace Aspire.Hosting;

public class Constants
{
    public const string SpinBinary = "spin";
    public const string SpinVariablePrefix = "SPIN_VARIABLE_";

    public static class SpinCommands
    {
        public const string Up = "up";
        public const string Registry = "registry";
        public const string Login = "login";
        public const string Plugins = "plugins";
        public const string Install = "install";
    }

    public static class SpinFlags
    {
        public const string Build = "--build";
        public const string From = "--from";
        public const string TlsCert = "--tls-cert";
        public const string TlsKey = "--tls-key";
        public const string DirectMounts = "--direct-mounts";
        public const string SqlLite = "--sqlite";
        public const string RuntimeConfigFile = "--runtime-config-file";
        public const string Version = "--version";
        public const string Listen = "--listen";
        public const string Confirm = "--yes";
    }
}