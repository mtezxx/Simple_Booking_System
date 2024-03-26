using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IResourceService
{
    Task<Resource> CreateAsync(ResourceCreationDto resourceToCreate);
    Task<List<Resource>> GetAsync(ResourceParametersDto searchParameters);
    
}