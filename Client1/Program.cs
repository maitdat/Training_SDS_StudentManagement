using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client1;
using Microsoft.AspNetCore.Components;
using Shared;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using ProtoBuf.Grpc.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(services =>
{
    // Get the service address from appsettings.json
    var backendUrl = "https://localhost:7087/";

    // If no address is set then fallback to the current webpage URL
    if (string.IsNullOrEmpty(backendUrl))
    {
        var navigationManager = services.GetRequiredService<NavigationManager>();
        backendUrl = navigationManager.BaseUri;
    }
    var httpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler());

    return GrpcChannel.ForAddress(
        backendUrl,
        new GrpcChannelOptions
        {
            HttpHandler = httpHandler,
        });
});


builder.Services.AddTransient<IMyService>(services =>
{
    var grpcChannel = services.GetRequiredService<GrpcChannel>();
    return grpcChannel.CreateGrpcService<IMyService>();
});

builder.Services.AddTransient<IStudentService>(services =>
{
    var grpcChannel = services.GetRequiredService<GrpcChannel>();
    return grpcChannel.CreateGrpcService<IStudentService>();
});

builder.Services.AddTransient<IClassService>(services =>
{
    var grpcChannel = services.GetRequiredService<GrpcChannel>();
    return grpcChannel.CreateGrpcService<IClassService>();
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAntDesign();

await builder.Build().RunAsync();
