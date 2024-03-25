using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;

namespace Shared.Models;

public class Booking
{
    public int Id { get; set; }
    public DateOnly DateFrom { get; set; }
    public DateOnly DateTo { get; set; }
    public int BookedQuantity { get; set; }
    public int ResourceId { get; set; }
    
    public Booking(DateOnly dateFrom, DateOnly dateTo, int bookedQuantity, int resourceId)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        BookedQuantity = bookedQuantity;
        ResourceId = resourceId;
    }
    
    public Booking(){}
}