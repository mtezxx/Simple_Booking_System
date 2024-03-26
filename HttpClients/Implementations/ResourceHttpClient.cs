using System.Collections;
using System.Net.Http.Json;
using System.Text.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;
using Shared.Models;

namespace HttpClients.Implementations;

public class ResourceHttpClient : IResourceService
{
    private readonly HttpClient client;
    
    public ResourceHttpClient(HttpClient client)
    {
        this.client = client;
    }
    
    public async Task<Resource> CreateAsync(ResourceCreationDto resourceToCreate)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/resources", resourceToCreate);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }
        
        Resource resource = JsonSerializer.Deserialize<Resource>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return resource;
    }

    public async Task<ICollection<Resource>> GetAsync(string? name = null, int? quantity = null)
    {
        string uri = "/resources";
        if (!string.IsNullOrEmpty(name))
        {
            uri += $"?name={name}";
        }
            
        if (quantity.HasValue)
        {
            uri += string.IsNullOrEmpty(name) ? $"?quantity={quantity}" : $"&quantity={quantity}";
        }

        HttpResponseMessage response = await client.GetAsync(uri);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Resource> resources = JsonSerializer.Deserialize<ICollection<Resource>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return resources;
    }
    
}