using FlightFinder;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddTransient<IFlight, Flight>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
