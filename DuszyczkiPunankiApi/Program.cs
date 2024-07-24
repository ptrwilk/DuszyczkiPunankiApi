using DuszyczkiPunankiApi.Data;
using DuszyczkiPunankiApi.Hubs;
using DuszyczkiPunankiApi.Routes;
using DuszyczkiPunankiApi.Services;
using Jwt.Core;
using Microsoft.EntityFrameworkCore;
using MikietaApi;

var builder = WebApplication.CreateBuilder(args);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IMainService, MainService>();
builder.Services.AddScoped<DbSeeder, DbSeeder>();
builder.Services.AddSingleton<ConfigurationOptions, ConfigurationOptions>();

builder.Services.AddDbContext<DataContext>((provider, options) =>
    options.UseNpgsql(provider.GetService<ConfigurationOptions>()!.Database));

builder.Services.AddSingleton<IJwtTokenFactory>(_ => new JwtTokenFactory(builder.Environment.IsDevelopment()
    ? builder.Configuration["Jwt:Key"]!
    : Environment.GetEnvironmentVariable("Jwt_Key")!));

builder.Services.AddAuthenticationWithJwt(builder.Environment.IsDevelopment()
    ? builder.Configuration["Jwt:Key"]!
    : Environment.GetEnvironmentVariable("Jwt_Key")!);

builder.Services.AddCors(options =>
    options.AddPolicy("MyPolicy",
        b => { b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }
    )
);

builder.Services.AddSignalR();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors("MyPolicy");

app.MapHub<MessageHub>("/messageHub");

MainRoute.RegisterEndpoints(app);
LoginRoute.RegisterEndpoints(app);

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetService<DbSeeder>();
seeder!.Seed();

app.Run();