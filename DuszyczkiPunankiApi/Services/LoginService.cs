using DuszyczkiPunankiApi.Data;
using DuszyczkiPunankiApi.Data.Entities;
using DuszyczkiPunankiApi.Models;
using Jwt.Core;
using Microsoft.EntityFrameworkCore;

namespace DuszyczkiPunankiApi.Services;

public interface ILoginService
{
    public LoginResponseModel Login(LoginRequestModel model);
}

public class LoginService : ILoginService
{
    private readonly DataContext _context;
    private readonly IJwtTokenFactory _jwtTokenFactory;

    public LoginService(DataContext context,
        IJwtTokenFactory jwtTokenFactory)
    {
        _context = context;
        _jwtTokenFactory = jwtTokenFactory;
    }

    public LoginResponseModel Login(LoginRequestModel model)
    {
        var user = _context.Users.AsEnumerable()
            .FirstOrDefault(x => x.Nickname.ToLower().Equals(model.Nickname.ToLower()));

        if (user is not null && user.Password != model.Password)
        {
            throw new InvalidOperationException("Nickname or password is not valid.");
        }

        var guid = user?.Id ?? Guid.NewGuid();
        if (user is null)
        {
            user = new UserEntity
            {
                Id = guid,
                Nickname = model.Nickname,
                Password = model.Password,
            };

            _context.Users.Add(user);

            _context.SaveChanges();
        }

        return new LoginResponseModel
        {
            Token = _jwtTokenFactory.Create(guid.ToString()),
        };
    }
}