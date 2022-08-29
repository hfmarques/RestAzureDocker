using WebApi.Data.Converter.Contract;
using WebApi.Data.Vo;
using WebApi.Models;

namespace WebApi.Data.Converter.Implementations;

public class PersonConverter : IParser<PersonVo, Person>, IParser<Person, PersonVo>
{
    public Person? Parse(PersonVo? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return new Person
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address = origin.Address,
            Gender = origin.Gender,
        };
    }

    public List<Person>? Parse(List<PersonVo>? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return origin.Select(Parse).ToList()!;
    }


    public PersonVo? Parse(Person? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return new PersonVo
        {
            Id = origin.Id,
            FirstName = origin.FirstName,
            LastName = origin.LastName,
            Address = origin.Address,
            Gender = origin.Gender,
        };
    }

    public List<PersonVo>? Parse(List<Person>? origin)
    {
        if (origin is null)
        {
            return null;
        }

        return origin.Select(Parse).ToList()!;
    }
}