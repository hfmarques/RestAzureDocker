using System.Data;
using WebApi.Data.Context;
using WebApi.Models;

namespace WebApi.Services.Implementations;

public class PersonService : IPersonService
{
    private readonly SqlServerContext context;

    public PersonService(SqlServerContext context)
    {
        this.context = context;
    }

    public Person Create(Person person)
    {
        context.People.Add(person);
        context.SaveChanges();
        return person;
    }

    public Person? FindById(long id)
    {
        return context.People.Find(id);
    }

    public List<Person> FindAll()
    {
        var people = context.People;

        return people.ToList();
    }

    public Person Update(Person person)
    {
        if (FindById(person.Id) is null)
            throw new DataException("Person do not exists");
        context.People.Update(person);
        context.SaveChanges();

        return person;
    }

    public void Delete(long id)
    {
        var person = FindById(id);
        if (person is null)
            return;
        context.People.Remove(person);
    }
}