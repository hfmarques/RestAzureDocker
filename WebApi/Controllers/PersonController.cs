using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Business;
using WebApi.Data.Vo;
using WebApi.HyperMedia.Filters;

namespace WebApi.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[Authorize("Bearer")]
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
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get()
    {
        return Ok(personBusiness.FindAll());
    }

    [HttpGet("{id:long}")]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Get(long id)
    {
        var person = personBusiness.FindById(id);

        if (person == null) return NotFound();

        return Ok(person);
    }

    [HttpPost]
    [TypeFilter(typeof(HyperMediaFilter))]
    public IActionResult Post(PersonVo? personVo)
    {
        if (personVo is null) return BadRequest();

        return Created("", personBusiness.Create(personVo));
    }

    [HttpPut]
    [TypeFilter(typeof(HyperMediaFilter))]
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