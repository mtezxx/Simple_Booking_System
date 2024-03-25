using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IResourceLogic
{
    Task<Resource> CreateAsync(ResourceCreationDto resourceToCreate);
}