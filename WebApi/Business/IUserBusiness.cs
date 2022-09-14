using WebApi.Models;

namespace WebApi.Business;

public interface IUserBusiness
{
    User? ValidateCredentials(User? user);
    User? ValidateCredentials(string username);
    bool RevokeToken(string username);
    void RefreshUserInfo(User user);
}