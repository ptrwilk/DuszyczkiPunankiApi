using System.Text.Json.Serialization;

namespace MikietaApi.Models.LobbyPlayer;

public class LobbyPlayerModel
{
    public Guid UserId { get; set; }
    public string Nickname { get; set; } = null!;
    public bool IsYou { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LobbyMessageType LobbyMessage { get; set; }
    public bool MessageSent { get; set; }
}