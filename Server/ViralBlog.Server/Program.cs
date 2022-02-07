using AspNetCore.Firebase.Authentication.Extensions;
using ViralBlog.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddFirebaseAuthentication("https://securetoken.google.com/gc-signalr", "gc-signalr");

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapHub<MessagingHub>("/messaging");

app.UseAuthentication();
app.UseAuthorization();

app.Run();
