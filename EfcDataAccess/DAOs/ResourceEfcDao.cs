using System.Collections;
using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.DTOs;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class ResourceEfcDao : IResourceDao
{
    private readonly BookingContext context;

    public ResourceEfcDao(BookingContext context)
    {
        this.context = context;
    }

    public async Task<Resource> CreateAsync(Resource resource)
    {
        EntityEntry<Resource> newResource = await context.Resources.AddAsync(resource);
        await context.SaveChangesAsync();
        return newResource.Entity;
    }

    public async Task<Resource?> GetByNameAsync(string name)
    {
        Resource? existing = await context.Resources.FirstOrDefaultAsync
            (r => r.Name.ToLower().Equals(name.ToLower()));
        return existing;
    }

    public async Task<Resource?> GetResourceByIdAsync(int id)
    {
        Resource? resource= await context.Resources.FindAsync(id);
        return resource;
    }

    public Task<ICollection> GetAsync(ResourceParametersDto searchParameters)
    {
        IQueryable<Resource> query = context.Resources.AsQueryable();

        if (!string.IsNullOrEmpty(searchParameters.Name))
        {
            query = query.Where(r =>
                r.Name.ToLower().Contains(searchParameters.Name.ToLower()));
        }

        if (searchParameters.Quantity != null)
        {
            query = query.Where(r => r.Quantity == searchParameters.Quantity);
        }
        
        List<Resource> result = query.ToList();
        return Task.FromResult<ICollection>(result);
    }
}   