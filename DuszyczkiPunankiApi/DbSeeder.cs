using DuszyczkiPunankiApi.Data;
using DuszyczkiPunankiApi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace MikietaApi;

public class DbSeeder
{
    private readonly DataContext _context;

    public DbSeeder(DataContext context)
    {
        _context = context;
    }
    
    public void Seed()
    {
        _context.Database.Migrate();

        if (!_context.Users.Any() && !_context.Lobbies.Any())
        {
            var user = new UserEntity
            {
                Nickname = "Punana",
                Password = "6969"
            };
            _context.Users.Add(user);

            _context.Lobbies.Add(new LobbyEntity
            {
                Name = "Duszyczki Punanci",
                Owner = user
            });
            
            _context.SaveChanges();
        }
    }
}