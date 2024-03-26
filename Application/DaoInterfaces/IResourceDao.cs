using Shared.Models;

namespace Application.DaoInterfaces;

public interface IResourceDao
{
    Task<Resource> CreateAsync(Resource resource);
    Task<Resource?> GetByNameAsync(string name);
}