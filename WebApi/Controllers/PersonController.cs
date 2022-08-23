using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> logger;
    private readonly IPersonService personService;

    public PersonController(ILogger<PersonController> logger, IPersonService personService)
    {
        this.logger = logger;
        this.personService = personService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(personService.FindAll());
    }

    [HttpGet("{id:long}")]
    public IActionResult Get(long id)
    {
        var person = personService.FindById(id);

        if (person == null) return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post(Person? person)
    {
        if (person is null) return BadRequest();

        return Created("", personService.Create(person));
    }

    [HttpPut]
    public IActionResult Put(Person? person)
    {
        if (person is null) return BadRequest();

        return Ok(personService.Update(person));
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        personService.Delete(id);

        return NoContent();
    }
}