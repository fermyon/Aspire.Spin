using Aspire.Hosting.ApplicationModel;

namespace Aspire.Hosting;

public static class SpinAppHostingExtension
{

    private static IResourceBuilder<SpinAppResource> BuildSpinAppResource(this IDistributedApplicationBuilder builder, string name, string workingDirectory,
        string[] args)
    {
        var resource = new SpinAppResource(name, workingDirectory);
        
        return builder.AddResource(resource)
            .WithUp()
            .WithSpinDefaults()
            .WithArgs(args);
    }
    
    public static IResourceBuilder<SpinAppResource> AddSpinApp(this IDistributedApplicationBuilder builder, string name, string workingDirectory, int port = 3000)
    {
        string[] effectiveArgs = BuildListenArgs(port);
        workingDirectory = Path.Combine(builder.AppHostDirectory, workingDirectory);
        return builder.BuildSpinAppResource(name, workingDirectory, effectiveArgs);
    }

    private static string[] BuildListenArgs(int port)
    {
        return [Constants.SpinFlags.Listen, $"127.0.0.1:{port}"];
    }

    private static String[] BuildOciArgs(OCIReference oci)
    {
        return [Constants.SpinFlags.From, oci.ToString()];
    }
    
    public static IResourceBuilder<SpinAppResource> AddSpinApp(this IDistributedApplicationBuilder builder, string name, OCIReference oci, int port = 3000)
    {
        string[] args = BuildListenArgs(port).Concat(BuildOciArgs(oci)).ToArray();
        return builder.BuildSpinAppResource(name, string.Empty, args);
    }

    private static IResourceBuilder<SpinAppResource> WithUp(this IResourceBuilder<SpinAppResource> builder)
    {
        return builder.WithArgs(Constants.SpinCommands.Up);
    }

    public static IResourceBuilder<SpinAppResource> WithSpinEnvironment(this IResourceBuilder<SpinAppResource> builder,
        IDictionary<string, string> vars)
    {
        vars.Keys.ToList().ForEach(key =>
        {
            builder.WithSpinEnvironment(key, vars[key]);
        });
        return builder;
    }
    
    public static IResourceBuilder<SpinAppResource> WithSpinEnvironment(this IResourceBuilder<SpinAppResource> builder, string name, string value)
    {
        return builder.WithEnvironment(Constants.SpinVariablePrefix + name, value);
    }
    
    public static IResourceBuilder<SpinAppResource> WithReference(this IResourceBuilder<SpinAppResource> builder, IResourceBuilder<IResourceWithConnectionString> source, string? connectionName = null, bool optional = false)
    {
        var name = connectionName;
        IResourceWithConnectionString resource = source.Resource;
        if (string.IsNullOrWhiteSpace(name))
        {
            name = resource.Name;
        }

        return builder.WithEnvironment(delegate (EnvironmentCallbackContext context)
        {
            var key = resource.ConnectionStringEnvironmentVariable ?? (Constants.SpinVariablePrefix + "ConnectionStrings__" + name);
            context.EnvironmentVariables[key] = new ConnectionStringReference(resource, optional);
        });
    }

    public static IResourceBuilder<SpinAppResource> WithRuntimeConfig(this IResourceBuilder<SpinAppResource> builder, RuntimeConfigurationBuilder runtimeConfigBuilder)
    {
        
        return builder.WithArgs(async (ctx) =>
        {
            var config = await runtimeConfigBuilder.Build();
            var runtimeConfigFile = Path.Combine(builder.ApplicationBuilder.AppHostDirectory, builder.Resource.WorkingDirectory, config.Name);
            await File.WriteAllTextAsync(runtimeConfigFile, config.ToToml());
            ctx.Args.Add(Constants.SpinFlags.RuntimeConfigFile);
            ctx.Args.Add(runtimeConfigFile);
        });
    }

    public static IResourceBuilder<SpinAppResource> WithSpinDefaults(this IResourceBuilder<SpinAppResource> builder)
    {
        return builder
            .WithArgs(Constants.SpinFlags.Build);
            
    }

    public static IResourceBuilder<SpinAppResource> WithSqlMigration(this IResourceBuilder<SpinAppResource> builder,
        string migration)
    {
        return builder
            .WithArgs(Constants.SpinFlags.SqlLite, migration);
    }

    public static IResourceBuilder<SpinAppResource> WithTlsCertificate(this IResourceBuilder<SpinAppResource> builder, string certPath, string keyPath)
    {
        return builder
            .WithArgs(Constants.SpinFlags.TlsCert, certPath)
            .WithArgs(Constants.SpinFlags.TlsKey, keyPath);
    }

    public static IResourceBuilder<SpinAppResource> WithDirectMounts(this IResourceBuilder<SpinAppResource> builder)
    {
        return builder.WithArgs(Constants.SpinFlags.DirectMounts);
    }
}