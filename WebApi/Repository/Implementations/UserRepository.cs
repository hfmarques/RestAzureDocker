using WebApi.Models;
using WebApi.Repository.Context;
using WebApi.Repository.Generic;

namespace WebApi.Repository.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly SqlServerContext context;

    public UserRepository(SqlServerContext context) : base(context)
    {
        this.context = context;
    }
}