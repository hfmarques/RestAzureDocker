using WebApi.Data.Vo;

namespace WebApi.Business;

public interface ILoginBusiness
{
    TokenVo? ValidateCredentials(UserVo userVo);
    TokenVo? ValidateCredentials(TokenVo userVo);
    bool RevokeToken(string username);
}