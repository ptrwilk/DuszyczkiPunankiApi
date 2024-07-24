using DuszyczkiPunankiApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using MikietaApi.Models.LobbyPlayer;

namespace DuszyczkiPunankiApi.Data;

public class DataContext : DbContext
{
    public DbSet<LobbyPlayerEntity> LobbyPlayers { get; set; }
    public DbSet<LobbyEntity> Lobbies { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LobbyPlayerEntity>()
            .Property(x => x.LobbyMessage)
            .HasConversion<string>();
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added).ToList();
        
        var maxLobbyPlayersIndex = LobbyPlayers.Any() ? LobbyPlayers.Max(x => x.Index) + 1 : 1;

        foreach (var entry in entries)
        {
            if (entry.Entity is LobbyPlayerEntity lobbyPlayer)
            {
                var number = maxLobbyPlayersIndex;

                lobbyPlayer.Index = number;
                maxLobbyPlayersIndex++;
            }
        }
        
        return base.SaveChanges();
    }
}