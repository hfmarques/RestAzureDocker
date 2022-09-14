using System.Security.Cryptography;
using System.Text;
using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Business.Implementations;

public class UserBusiness : IUserBusiness
{
    private readonly IUserRepository userRepository;

    public UserBusiness(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public User? ValidateCredentials(User? user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        var pass = ComputeHash(user.Password);
        return userRepository.Find(x =>
                x.UserName == user.UserName &&
                x.Password == pass)
            .FirstOrDefault();
    }

    public User? ValidateCredentials(string username)
    {
        var dbUser = userRepository.Find(x => x.UserName == username).FirstOrDefault();

        return dbUser;
    }

    public bool RevokeToken(string username)
    {
        var user = userRepository.Find(x => x.UserName == username).FirstOrDefault();

        if (user is null) return false;

        user.RefreshToken = null;
        userRepository.Update(user);

        return true;
    }

    public void RefreshUserInfo(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        userRepository.Update(user);
    }

    private string ComputeHash(string strData)
    {
        var message = Encoding.UTF8.GetBytes(strData);
        using var alg = SHA512.Create();

        var hashValue = alg.ComputeHash(message);

        return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
    }
}