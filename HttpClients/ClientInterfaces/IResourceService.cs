using Shared.DTOs;
using Shared.Models;

namespace HttpClients.ClientInterfaces;

public interface IResourceService
{
    Task<Resource> CreateAsync(ResourceCreationDto resourceToCreate);
    Task<ICollection<Resource>> GetAsync(string? name = null, int? quantity = null);
    
}