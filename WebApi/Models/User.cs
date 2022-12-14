namespace WebApi.Models;

#nullable disable
public class User
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Password { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryType { get; set; }
}