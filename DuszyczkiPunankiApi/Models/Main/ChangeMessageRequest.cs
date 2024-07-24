using MikietaApi.Models.LobbyPlayer;

namespace MikietaApi.Models.Main;

public class ChangeMessageRequest
{
    public LobbyMessageType LobbyMessage { get; set; }
}