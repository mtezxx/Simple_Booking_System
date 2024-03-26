using System.Collections;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ResourcesController :ControllerBase
{
    private readonly IResourceLogic resourceLogic;
    
    public ResourcesController(IResourceLogic resourceLogic)
    {
        this.resourceLogic = resourceLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Resource>> CreateAsync(ResourceCreationDto dto)
    {
        try
        {
            Resource resource = await resourceLogic.CreateAsync(dto);
            return Created($"/resources/{resource.Id}", resource);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection>> GetAsync([FromQuery] string? name, [FromQuery] int? quantity)
    {
        try
        {
            ResourceParametersDto parameters = new(name, quantity);
            ICollection resources = await resourceLogic.GetAsync(parameters);
            return Ok(resources);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}