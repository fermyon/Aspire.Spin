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

var rtc = SpinRuntimeConfigurationBuilder.Create("myruntimeconfig.toml")
    .WithRedisKeyValueStore("default", redis);

builder.AddSpinApp("api-one", "../api-one", 3001)
    .WithOtlpExporter();

builder.AddSpinApp("api-two", "../api-two", 3002)
    .WithRuntimeConfig(rtc)
    .WithSpinEnvironment("Foo", "Bar")
    .WithOtlpExporter();

builder.AddSpinApp("api-three", OciReference.From("thorstenhans/spin-ts-app", "0.0.1"), 3003)
    .WithOtlpExporter();

builder.Build().Run();