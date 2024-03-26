using Shared.DTOs;

namespace HttpClients.ClientInterfaces;

public interface IBookingService
{
    Task BookResourceAsync(BookingCreationDto bookingToCreate);
}