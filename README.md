# Fermyon.Aspire.Hosting.Spin

`Fermyon.Aspire.Hosting.Spin` adds support for [Spin](https://developer.fermyon.com/spin) to [.NET Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/build-your-first-aspire-app). 

The project is under active development, so no guarantees of stability or compatibility can be expected - but we still hope you think it's great!

## Installation

```bash
# Add Aspire.Hosting.Spin to your .NET Aspire project
dotnet add package Fermyon.Aspire.Hosting.Spin
```

### Basic Usage

`Fermyon.Aspire.Hosting.Spin` contributes extensions to the `Aspire.Hosting` namespace. Adding an existing Spin App to your `DistributedApplicationBuilder` is as simple as calling `AddSpinApp`:

```csharp
var builder = DistributedApplication.CreateBuilder(args);

builder.AddSpinApp("api-one", "../api-one", 3001)
    .WithOtlpExporter();
builder.Build().Run();
```



### Further Samples

Check out the [example](./example/) folder, it contains a basic sample illustrating how you can design a distributed application using multiple Spin Apps
