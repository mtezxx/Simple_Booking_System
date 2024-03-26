using System.Collections;
using Shared.DTOs;
using Shared.Models;

namespace Application.DaoInterfaces;

public interface IResourceDao
{
    Task<Resource> CreateAsync(Resource resource);
    Task<Resource?> GetByNameAsync(string name);
    Task<Resource?> GetResourceByIdAsync(int id);
    Task<ICollection> GetAsync(ResourceParametersDto searchParameters);
}