namespace Shared.DTOs;

public class BookingCreationDto
{
    public DateOnly DateFrom { get; }
    public DateOnly DateTo { get; }
    public int BookedQuantity { get; }
    public int ResourceId { get; }
    
    public BookingCreationDto(DateOnly dateFrom, DateOnly dateTo, int bookedQuantity, int resourceId)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        BookedQuantity = bookedQuantity;
        ResourceId = resourceId;
    }
}