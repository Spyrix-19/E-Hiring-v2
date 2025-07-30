using Aspire.Hosting;
using BFPR4B.EHiring.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.BFPR4B_EHiring_ApiService>("ApiService")
    //.WithExternalHttpEndpoints()
    .WithSwaggerUI()
    .WithScalar()
    .WithReDoc();


var vueApp = builder.AddNpmApp("Web", "../BFPR4B.EHiring.Web", "dev")
                     .WithReference(apiService)
                     .WaitFor(apiService)
                     .WithHttpEndpoint(env: "DEV_SERVER_PORT")
                     .WithExternalHttpEndpoints();

//builder.AddExecutable("browser-launcher", "powershell", $"-Command \"Start-Process '{vueApp.GetAddress()}'\"")
//       .WaitFor(vueApp); // Ensure it only runs AFTER the Vue app is ready

builder.Build().Run();
