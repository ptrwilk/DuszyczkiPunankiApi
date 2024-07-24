namespace DuszyczkiPunankiApi.Data.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Nickname { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public ICollection<LobbyPlayerEntity>? LobbyPlayers { get; set; }
    public ICollection<LobbyEntity>? Lobbies { get; set; }
}