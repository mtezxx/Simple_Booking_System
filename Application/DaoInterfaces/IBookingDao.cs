using Shared.Models;

namespace Application.DaoInterfaces;

public interface IBookingDao
{
    Task<Booking> BookResourceAsync(Booking booking);
    Task<IEnumerable<Booking>> GetBookingsByResourceIdAsync(int resourceId);
}