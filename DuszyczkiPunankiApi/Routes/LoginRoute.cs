using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DuszyczkiPunankiApi.Models;
using DuszyczkiPunankiApi.Services;
using Jwt.Core;
using Microsoft.AspNetCore.Authorization;

namespace DuszyczkiPunankiApi.Routes;

public static class LoginRoute
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        app.MapPost("login", Login);
        app.MapGet("login/check", CheckLogin);

        return app;
    }

    private static IResult Login(ILoginService loginService, LoginRequestModel model)
    {
        try
        {
            return Results.Ok(loginService.Login(model));
        }
        catch (Exception ex)
        {
            return Results.Conflict(ex.Message);
        }
    }
    
    [Authorize]
    private static IResult CheckLogin(ILoginService loginService)
    {
        return Results.Ok(true);
    }
}