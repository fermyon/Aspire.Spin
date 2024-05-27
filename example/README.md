# Example of Aspire with Spin apps

This example shows ho you can run Spin apps using the .NET Aspire framework with the Aspire.Hosting.Spin package.

The examples contains services in a few different languages, using Spin's Key-Value API to host data in a Redis container, as well as running Spin apps from an OCI registry.

Please note, for Open Telemetry to work, for now you'll have to run Aspire using http: `ASPIRE_ALLOW_UNSECURED_TRANSPORT=true dotnet run --project host/Aspire.Host.csproj --launch-profile http`
