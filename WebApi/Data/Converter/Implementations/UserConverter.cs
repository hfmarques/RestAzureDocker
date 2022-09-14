using WebApi.Data.Converter.Contract;
using WebApi.Data.Vo;
using WebApi.Models;

namespace WebApi.Data.Converter.Implementations;

public class UserConverter : IParser<UserVo, User>, IParser<User, UserVo>
{
    public User? Parse(UserVo? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return new User
        {
            Id = origin.Id ?? 0,
            UserName = origin.UserName,
            FullName = origin.FullName,
            Password = origin.Password,
            RefreshToken = origin.RefreshToken,
            RefreshTokenExpiryType = origin.RefreshTokenExpiryType ?? DateTime.Now
        };
    }

    public List<User>? Parse(List<UserVo>? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return origin.Select(Parse).ToList()!;
    }


    public UserVo? Parse(User? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return new UserVo
        {
            Id = origin.Id,
            UserName = origin.UserName,
            FullName = origin.FullName,
            Password = origin.Password,
            RefreshToken = origin.RefreshToken,
        };
    }

    public List<UserVo>? Parse(List<User>? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return origin.Select(Parse).ToList()!;
    }
}