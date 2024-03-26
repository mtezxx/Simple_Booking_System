using System.Collections;
using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IResourceLogic
{
    Task<Resource> CreateAsync(ResourceCreationDto resourceToCreate);
    Task<ICollection> GetAsync(ResourceParametersDto searchParameters);
}