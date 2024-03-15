using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using CoreWCF.Description;
using FlightProvider;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();


var app = builder.Build();
app.UseServiceModel(bld =>
{
    bld.AddService<AirSearch>();
    bld.AddService<AirportSearch>();
    bld.AddServiceEndpoint<AirSearch, IAirSearch>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service.svc");
    bld.AddServiceEndpoint<AirportSearch, IAirportSearch>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/Service2.svc");
    var mb = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    mb.HttpsGetEnabled = true;
});

//app.MapGet("/", () => "Hello World!");

app.Run();
