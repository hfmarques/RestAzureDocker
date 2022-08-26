using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Models;

namespace WebApi.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> logger;
    private readonly IPersonBusiness personBusiness;

    public PersonController(ILogger<PersonController> logger, IPersonBusiness personBusiness)
    {
        this.logger = logger;
        this.personBusiness = personBusiness;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(personBusiness.FindAll());
    }

    [HttpGet("{id:long}")]
    public IActionResult Get(long id)
    {
        var person = personBusiness.FindById(id);

        if (person == null) return NotFound();

        return Ok(person);
    }

    [HttpPost]
    public IActionResult Post(Person? person)
    {
        if (person is null) return BadRequest();

        return Created("", personBusiness.Create(person));
    }

    [HttpPut]
    public IActionResult Put(Person? person)
    {
        if (person is null) return BadRequest();

        return Ok(personBusiness.Update(person));
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        personBusiness.Delete(id);

        return NoContent();
    }
}