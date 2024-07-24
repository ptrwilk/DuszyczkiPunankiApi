using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using DuszyczkiPunankiApi.Hubs;
using DuszyczkiPunankiApi.Services;
using Jwt.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using MikietaApi.Models.LobbyPlayer;

namespace DuszyczkiPunankiApi.Routes;

public static class MainRoute
{
    public static WebApplication RegisterEndpoints(WebApplication app)
    {
        app.MapGet("/", Get);
        app.MapPut("/start", Start);
        app.MapPut("/stop", Stop);
        app.MapPut("", Update);

        return app;
    }

    // [Authorize]
    private static IResult Get(
        HttpContext context,
        IMainService mainService,
        IHubContext<MessageHub, IMessageHub> hub,
        ClaimsPrincipal claims)
    {
        var userId = claims.GetUserId() ?? throw new Exception("User Id not found");
        
        return Results.Ok(mainService.Get(userId));
    }

    private static IResult Start(
        IMainService mainService,
        IHubContext<MessageHub, IMessageHub> hub,
        ClaimsPrincipal claims)
    {
        try
        {
            var userId = claims.GetUserId() ?? throw new Exception("User Id not found");

            var res = mainService.Start(userId);
            
            hub.Clients.All.Refresh(userId.ToString());

            return Results.Ok(res);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    private static IResult Stop(
        IMainService mainService,
        IHubContext<MessageHub, IMessageHub> hub,
        ClaimsPrincipal claims)
    {
        try
        {
            var userId = claims.GetUserId() ?? throw new Exception("User Id not found");

            var res = mainService.Stop(userId);
            
            hub.Clients.All.Refresh(userId.ToString());

            return Results.Ok(res);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
    
    private static IResult Update(
        IMainService mainService,
        IHubContext<MessageHub, IMessageHub> hub,
        ClaimsPrincipal claims,
        LobbyPlayerModel model)
    {
        try
        {
            var userId = claims.GetUserId() ?? throw new Exception("User Id not found");

            var res = mainService.Update(userId, model);
            
            hub.Clients.All.Refresh(userId.ToString());

            return Results.Ok(res);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}