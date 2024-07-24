using MikietaApi.Models.LobbyPlayer;

namespace MikietaApi.Models.Main;

public class MainModel
{
    public LobbyPlayerModel[]? LobbyPlayers { get; set; }
    
    public Guid UserId { get; set; }
    public bool IsLobbyOwner { get; set; }
    public bool IsInLobby { get; set; }
    public bool IsOnline { get; set; }
    public bool MessageSent { get; set; }
}