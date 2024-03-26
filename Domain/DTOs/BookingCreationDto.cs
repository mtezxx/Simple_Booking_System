namespace Shared.DTOs;

public class BookingCreationDto
{
    public DateOnly DateFrom { get; init; }
    public DateOnly DateTo { get; init; }
    public int BookedQuantity { get; init; }
    public int ResourceId { get; init; }

    public BookingCreationDto(DateOnly dateFrom, DateOnly dateTo, int bookedQuantity, int resourceId)
    {
        DateFrom = dateFrom;
        DateTo = dateTo;
        BookedQuantity = bookedQuantity;
        ResourceId = resourceId;
    }
}