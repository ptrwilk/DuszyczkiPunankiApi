var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => new [] { 1, 2, 3 });

app.Run();