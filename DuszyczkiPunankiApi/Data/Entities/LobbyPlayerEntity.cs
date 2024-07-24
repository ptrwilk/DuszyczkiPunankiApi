using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MikietaApi.Models.LobbyPlayer;

namespace DuszyczkiPunankiApi.Data.Entities;

public class LobbyPlayerEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public Guid UserId { get; set; }
    public UserEntity User { get; set; } = null!;
    public LobbyMessageType LobbyMessage { get; set; }
    public bool MessageSent { get; set; }
    public int Index { get; set; }
}