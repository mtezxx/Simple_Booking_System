using Shared.Models;

namespace Application.Shared;


    public static class BookingHelper
    {
        public static bool IsConflict(IEnumerable<Booking> existingBookings, DateOnly requestedStart, DateOnly requestedEnd)
        {
            return existingBookings.Any(b => !(requestedStart >= b.DateTo || requestedEnd <= b.DateFrom));
        }
    }