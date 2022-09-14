using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApi.Configurations;
using WebApi.Data.Converter.Implementations;
using WebApi.Data.Vo;

namespace WebApi.Business.Implementations;

public class LoginBusiness : ILoginBusiness
{
    private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
    private readonly TokenConfiguration configuration;
    private readonly IUserBusiness userBusiness;
    private readonly ITokenBusiness tokenBusiness;
    private readonly UserConverter converter;

    public LoginBusiness(
        TokenConfiguration configuration,
        IUserBusiness userBusiness,
        ITokenBusiness tokenBusiness,
        UserConverter converter
    )
    {
        this.configuration = configuration;
        this.userBusiness = userBusiness;
        this.tokenBusiness = tokenBusiness;
        this.converter = converter;
    }

    public TokenVo? ValidateCredentials(UserVo userVo)
    {
        var user = userBusiness.ValidateCredentials(converter.Parse(userVo));
        if (user is null)
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(JwtRegisteredClaimNames.UniqueName, user.UserName)
        };

        var accessToken = tokenBusiness.GenerateAccessToken(claims);
        var refreshToken = tokenBusiness.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryType = DateTime.Now.AddDays(configuration.DaysToExpiry);

        userBusiness.RefreshUserInfo(user);

        var createDate = DateTime.Now;
        var expirationDate = createDate.AddMinutes(configuration.Minutes);

        return new TokenVo
        {
            Authenticated = true,
            Created = createDate.ToString(DATE_FORMAT),
            Expiration = expirationDate.ToString(DATE_FORMAT),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public TokenVo? ValidateCredentials(TokenVo tokenVo)
    {
        var accessToken = tokenVo.AccessToken;

        var principal = tokenBusiness.GetPrincipalFromExpiredToken(accessToken);

        var username = principal.Identity?.Name;
        
        if (username is null) return null;
        
        var user = userBusiness.ValidateCredentials(username);

        if (user is null ||
            user.RefreshToken != tokenVo.RefreshToken ||
            user.RefreshTokenExpiryType <= DateTime.Now)
        {
            return null;
        }

        accessToken = tokenBusiness.GenerateAccessToken(principal.Claims);
        var refreshToken = tokenBusiness.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        var createDate = DateTime.Now;
        var expirationDate = createDate.AddMinutes(configuration.Minutes);

        userBusiness.RefreshUserInfo(user);

        return new TokenVo
        {
            Authenticated = true,
            Created = createDate.ToString(DATE_FORMAT),
            Expiration = expirationDate.ToString(DATE_FORMAT),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }

    public bool RevokeToken(string username)
    {
        return userBusiness.RevokeToken(username);
    }
}