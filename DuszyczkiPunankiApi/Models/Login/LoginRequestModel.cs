namespace DuszyczkiPunankiApi.Models;

public class LoginRequestModel
{
    public string Nickname { get; set; } = null!;
    public string Password { get; set; } = null!;
}