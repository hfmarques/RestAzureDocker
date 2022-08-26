using WebApi.Models;
using WebApi.Repository;

namespace WebApi.Business.Implementations;

public class PersonBusiness : IPersonBusiness
{
    private readonly IPersonRepository personRepository;

    public PersonBusiness(IPersonRepository personRepository)
    {
        this.personRepository = personRepository;
    }

    public Person Create(Person person)
    {
        return personRepository.Add(person);
    }

    public Person? FindById(long id)
    {
        return personRepository.Get(id);
    }

    public List<Person> FindAll()
    {
        return personRepository.GetAll().ToList();
    }

    public Person Update(Person person)
    {
        personRepository.Update(person);
        return person;
    }

    public void Delete(long id)
    {
        var person = personRepository.Get(id);
        if (person != null) personRepository.Remove(person);
    }
}