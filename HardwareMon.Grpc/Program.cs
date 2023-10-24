using HardwareMon;
using HardwareMon.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Processor>();
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<HardwareService>();

app.Run();
