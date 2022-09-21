using WebApi.Models;
using WebApi.Repository.Context;
using WebApi.Repository.Generic;

namespace WebApi.Repository.Implementations;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    private readonly MySqlContext context;

    public PersonRepository(MySqlContext context) : base(context)
    {
        this.context = context;
    }
}