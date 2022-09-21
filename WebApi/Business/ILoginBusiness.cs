using WebApi.Data.Vo;

namespace WebApi.Business;

public interface ILoginBusiness
{
    void NewUser(UserVo userVo);
    TokenVo? ValidateCredentials(UserVo userVo);
    TokenVo? ValidateCredentials(TokenVo userVo);
    bool RevokeToken(string username);
}