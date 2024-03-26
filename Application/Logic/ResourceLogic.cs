using System.Collections;
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
        Resource? existing = await resourceDao.GetByNameAsync(resourceToCreate.Name);
        if (existing != null)
        {
            throw new Exception("Resource with this name already exists! ");
        }
        
        ValidateData(resourceToCreate);
        Resource toCreate = new Resource
        {
            Name = resourceToCreate.Name,
            Quantity = resourceToCreate.Quantity
        };
        
        Resource created = await resourceDao.CreateAsync(toCreate);
        
        return created;
    }

    public Task<ICollection> GetAsync(ResourceParametersDto searchParameters)
    {
        return resourceDao.GetAsync(searchParameters);
    }

    public static void ValidateData(ResourceCreationDto resourceToCreate)
    {
        string name = resourceToCreate.Name;
        int quantity = resourceToCreate.Quantity;
        
        if (name.Length <= 1)
        {
            throw new Exception("Name must contain at least 1 character! ");
        }
        else if (name.Length >= 30)
        {
            throw new Exception("Name must be less then 30 characters! ");
        }
        
        if (quantity <= 0)
        {
            throw new Exception("Quantity must be greater than 0! ");
        }
    }
}