using Shared.Models;

namespace Application.Shared;

public static class BookingHelper
{
    // Adding quantity parameters to the method
    public static bool IsConflict(IEnumerable<Booking> existingBookings, DateOnly requestedStart, DateOnly requestedEnd, int bookedQuantityDuringPeriod, int resourceQuantity)
    {
        if (requestedStart < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new Exception("Booking cannot start before the current day.");
        }
        
        // Adjusting the conflict logic to consider remaining quantity
        int availableQuantity = resourceQuantity - bookedQuantityDuringPeriod;
        return existingBookings.Any(b => !(requestedStart > b.DateTo || requestedEnd < b.DateFrom) && availableQuantity <= 0);
    }
}