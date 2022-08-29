using WebApi.Data.Converter.Implementations;
using WebApi.Data.Vo;
using WebApi.Repository;

namespace WebApi.Business.Implementations;

public class PersonBusiness : IPersonBusiness
{
    private readonly IPersonRepository personRepository;
    private readonly PersonConverter converter;

    public PersonBusiness(IPersonRepository personRepository, PersonConverter converter)
    {
        this.personRepository = personRepository;
        this.converter = converter;
    }

    public PersonVo Create(PersonVo personVo)
    {
        if (personVo is null)
            throw new ArgumentNullException(nameof(personVo));

        var person = converter.Parse(personVo);

        return converter.Parse(personRepository.Add(person!))!;
    }

    public PersonVo? FindById(long id)
    {
        var person = personRepository.Get(id);
        return person is null ? null : converter.Parse(person);
    }

    public List<PersonVo> FindAll()
    {
        var people = personRepository.GetAll().ToList();
        return converter.Parse(people)!;
    }

    public PersonVo Update(PersonVo personVo)
    {
        if (personVo is null)
            throw new ArgumentNullException(nameof(personVo));
        var person = converter.Parse(personVo);
        personRepository.Update(person!);
        return personVo;
    }

    public void Delete(long id)
    {
        var person = personRepository.Get(id);
        if (person != null) personRepository.Remove(person);
    }
}