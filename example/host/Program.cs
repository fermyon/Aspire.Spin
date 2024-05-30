using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

// Lifecycle Hooks we provide
builder.Services.AddScoped<CheckForSpin>();
// builder.Services.AddScoped<InstallSpinPlugin>(sp => new InstallSpinPlugin("cloud"));
// builder.Services.AddScoped<SpinRegistryLogin>(sp =>
// {
//     var credentials = new OciRegistryCredentials();
//     builder.Configuration.Bind("OciRegistryCredentials", credentials);
//     return new SpinRegistryLogin(credentials);
// });

var redis = builder.AddRedis("redis").WithOtlpExporter();

var redisRuntimeConfig = SpinRuntimeConfigurationBuilder.Create("myruntimeconfig.toml")
    .WithRedisKeyValueStore("default", redis);

builder.AddSpinApp("api-one", "../api-one", 3001)
    .WithOtlpExporter();

builder.AddSpinApp("api-two", "../api-two", 3002)
    .WithRuntimeConfig(redisRuntimeConfig)
    .WithSpinEnvironment("Foo", "Bar")
    .WithOtlpExporter();

builder.AddSpinApp("api-three", OciReference.From("thorstenhans/spin-ts-app", "0.0.1"), 3003)
    .WithOtlpExporter();

var cloudGpuUrl = builder.Configuration.GetValue<string>("CloudGpuUrl");
var cloudGpuToken = builder.Configuration.GetValue<string>("CloudGpuToken");

if (!string.IsNullOrWhiteSpace(cloudGpuUrl) &&
    !string.IsNullOrWhiteSpace(cloudGpuToken))
{
    var llmRuntimeConfig = SpinRuntimeConfigurationBuilder.Create("llm.toml")
        .WithLLM(cloudGpuUrl, cloudGpuToken);
    builder.AddSpinApp("api-four", "../api-four", 3004)
        .WithRuntimeConfig(llmRuntimeConfig)
        .WithOtlpExporter();
}

builder.Build().Run();