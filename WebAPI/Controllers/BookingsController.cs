using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingLogic bookingLogic;
    
    public BookingsController(IBookingLogic bookingLogic)
    {
        this.bookingLogic = bookingLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<BookingResponseDto>> BookResourceAsync([FromBody] BookingCreationDto dto)
    {
        try
        {
            BookingResponseDto created = await bookingLogic.BookResourceAsync(dto);
            return Created($"/bookings/{created.BookingId}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}