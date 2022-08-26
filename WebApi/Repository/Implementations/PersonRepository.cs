using WebApi.Data.Context;
using WebApi.Models;
using WebApi.Repository.Generic;

namespace WebApi.Repository.Implementations;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    private readonly SqlServerContext context;

    public PersonRepository(SqlServerContext context) : base(context)
    {
        this.context = context;
    }
}