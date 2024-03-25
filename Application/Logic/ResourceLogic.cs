using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace Application.Logic;

public class ResourceLogic : IResourceLogic
{
    private readonly IResourceDao resourceDao;
    
    public ResourceLogic(IResourceDao resourceDao)
    {
        this.resourceDao = resourceDao;
    }
    
    public async Task<Resource> CreateAsync(ResourceCreationDto resourceToCreate)
    {
        throw new NotImplementedException();
    }
}