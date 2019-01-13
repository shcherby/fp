namespace FP.TimeTracking.Core

module AppSettingsProvider = 
    open Microsoft.Extensions.Configuration
    open System.IO

    let getConnectionString _ =
        let builder = ((new ConfigurationBuilder(), Directory.GetCurrentDirectory())
                      |> FileConfigurationExtensions.SetBasePath, "appsettings.json")
                      |> JsonConfigurationExtensions.AddJsonFile

        builder.Build().GetConnectionString("Storage")

