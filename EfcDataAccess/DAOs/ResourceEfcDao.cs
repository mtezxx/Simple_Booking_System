using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
}