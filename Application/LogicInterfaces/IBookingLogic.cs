using Shared.DTOs;
using Shared.Models;

namespace Application.LogicInterfaces;

public interface IBookingLogic
{
    Task<BookingResponseDto> BookResourceAsync(BookingCreationDto bookingToCreate);
}