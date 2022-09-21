using WebApi.Models;
using WebApi.Repository.Context;
using WebApi.Repository.Generic;

namespace WebApi.Repository.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly MySqlContext context;

    public UserRepository(MySqlContext context) : base(context)
    {
        this.context = context;
    }
}