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

    public async Task<List<Resource>> GetAsync(ResourceParametersDto searchParameters)
    {
        string query = ConstructQuery(searchParameters.Name, searchParameters.Quantity);

        HttpResponseMessage response = await client.GetAsync("/resources"+query);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Resource> resources = JsonSerializer.Deserialize<ICollection<Resource>>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return (List<Resource>)resources;
    }
    
    
    private static string ConstructQuery(string? name, int? quantity)
    {
        string query = "";
        if (!string.IsNullOrEmpty(name))
        {
            query += $"?name={name}";
        }

        if (quantity != null)
        {
            query += string.IsNullOrEmpty(query) ? "?" : "&";
            query += $"quantity={quantity}";
        }
        return query;
    }
}   