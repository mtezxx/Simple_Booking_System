namespace Shared.DTOs;

public class ResourceCreationDto
{
    public string Name { get;  }
    public int Quantity { get;  }
    
    public ResourceCreationDto(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}