namespace Shared.DTOs;

public class ResourceParametersDto
{
    public string? Name { get;  }
    public int? Quantity { get;  }
    
    public ResourceParametersDto(string? name, int? quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}