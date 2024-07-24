using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DuszyczkiPunankiApi.Data.Entities;

public class LobbyEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    [ForeignKey(nameof(OwnerId))]
    public Guid OwnerId { get; set; }
    public UserEntity Owner { get; set; } = null!;

}