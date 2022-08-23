using WebApi.Models;

namespace WebApi.Services.Implementations;

public class PersonService : IPersonService
{
    public Person Create(Person person)
    {
        return person;
    }

    public Person? FindById(long id)
    {
        return new Person
        {
            Id = 1,
            FirstName = "Heber",
            LastName = "Marques",
            Address = "Rio de Janeiro",
            Gender = "Male"
        };
    }

    public List<Person> FindAll()
    {
        return new List<Person>
        {
            new()
            {
                Id = 1,
                FirstName = "Heber",
                LastName = "Marques",
                Address = "Rio de Janeiro",
                Gender = "Male"
            },
            new()
            {
                Id = 2,
                FirstName = "Hellen",
                LastName = "Miranda",
                Address = "Rio de Janeiro",
                Gender = "Female"
            }
        };
    }

    public Person Update(Person person)
    {
        return person;
    }

    public void Delete(long id)
    {
    }
}