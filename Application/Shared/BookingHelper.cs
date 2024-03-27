using Shared.Models;

namespace Application.Shared;

public static class BookingHelper
{
    public static bool IsConflict(IEnumerable<Booking> existingBookings, DateOnly requestedStart, DateOnly requestedEnd)
    {
        if (requestedStart < DateOnly.FromDateTime(DateTime.Now))
        {
            throw new Exception("Booking cannot start before the current day.");
        }

        return existingBookings.Any(b => !(requestedStart >= b.DateTo || requestedEnd <= b.DateFrom));
    }
}