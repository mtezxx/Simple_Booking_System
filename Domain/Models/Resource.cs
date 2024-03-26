using System.ComponentModel.DataAnnotations;

namespace Shared.Models;

public class Resource
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [Range(1, int.MaxValue, ErrorMessage = "At least one item must be added.")]
    public int Quantity { get; set; }

    public Resource(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
    
    public Resource(){}
}