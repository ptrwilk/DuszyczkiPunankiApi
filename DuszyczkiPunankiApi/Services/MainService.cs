using DuszyczkiPunankiApi.Data;
using DuszyczkiPunankiApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using MikietaApi.Models.LobbyPlayer;
using MikietaApi.Models.Main;

namespace DuszyczkiPunankiApi.Services;

public interface IMainService
{
    MainModel Get(Guid userId);
    MainModel Start(Guid userId);
    MainModel Stop(Guid userId);
    MainModel Update(Guid userId, LobbyPlayerModel model);
}

public class MainService : IMainService
{
    private readonly DataContext _context;

    public MainService(DataContext context)
    {
        _context = context;
    }

    public MainModel Get(Guid userId)
    {
        var isInLobby = _context.LobbyPlayers.Any(x => x.UserId == userId);
        var isLobbyOwner = IsLobbyOwner(userId);
        var isOnline = IsOnline();
        var lobbyOwner = _context.Lobbies.First();
        var lobbyPlayer = _context.LobbyPlayers.FirstOrDefault(x => x.UserId == userId);

        LobbyPlayerModel[]? lobbyPlayers = null;
        if (isOnline)
        {
            lobbyPlayers = _context.LobbyPlayers.Include(x => x.User)
                .Where(x => x.UserId != lobbyOwner.OwnerId)
                .OrderBy(x => x.Index)
                .Select(x => new LobbyPlayerModel
                {
                    UserId = x.UserId,
                    IsYou = x.UserId == userId,
                    Nickname = x.User.Nickname,
                    LobbyMessage = x.LobbyMessage,
                    MessageSent = x.MessageSent
                }).ToArray();
        }

        return new MainModel
        {
            UserId = userId,
            IsInLobby = isInLobby,
            IsLobbyOwner = isLobbyOwner,
            IsOnline = isOnline,
            LobbyPlayers = lobbyPlayers,
            MessageSent = lobbyPlayer?.MessageSent ?? false
        };
    }

    public MainModel Start(Guid userId)
    {
        if (!IsLobbyOwner(userId) && !IsOnline())
        {
            throw new Exception("Owner has not started a lobby.");
        }

        if (_context.LobbyPlayers.All(x => x.UserId != userId))
        {
            _context.LobbyPlayers.Add(new LobbyPlayerEntity
            {
                UserId = userId,
                LobbyMessage = LobbyMessageType.Question
            });

            _context.SaveChanges();
        }

        return Get(userId);
    }

    public MainModel Stop(Guid userId)
    {
        if (!IsLobbyOwner(userId) && !IsOnline())
        {
            throw new Exception("Owner has not started a lobby.");
        }

        var lobbyPlayer = _context.LobbyPlayers.FirstOrDefault(x => x.UserId == userId);

        if (lobbyPlayer is not null)
        {
            _context.LobbyPlayers.Remove(lobbyPlayer);

            _context.SaveChanges();
        }

        return Get(userId);
    }

    public MainModel Update(Guid userId, LobbyPlayerModel model)
    {
        var lobbyPlayer = _context.LobbyPlayers.First(x => x.UserId == model.UserId);

        lobbyPlayer.LobbyMessage = model.LobbyMessage;
        lobbyPlayer.MessageSent = model.MessageSent;

        _context.SaveChanges();

        return Get(userId);
    }

    private bool IsOnline()
    {
        var ownerId = _context.Lobbies.First().OwnerId;
        return _context.LobbyPlayers.Any(x => x.UserId == ownerId);
    }

    private bool IsLobbyOwner(Guid userId)
    {
        return _context.Lobbies.Any(x => x.OwnerId == userId);
    }
}