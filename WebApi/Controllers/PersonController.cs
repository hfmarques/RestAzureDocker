using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Data.Vo;

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
    public IActionResult Post(PersonVo? personVo)
    {
        if (personVo is null) return BadRequest();

        return Created("", personBusiness.Create(personVo));
    }

    [HttpPut]
    public IActionResult Put(PersonVo? personVo)
    {
        if (personVo is null) return BadRequest();

        return Ok(personBusiness.Update(personVo));
    }

    [HttpDelete("{id:long}")]
    public IActionResult Delete(long id)
    {
        personBusiness.Delete(id);

        return NoContent();
    }
}