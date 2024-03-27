using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;
using Application.Shared;

namespace Application.Logic
{
    public class BookingLogic : IBookingLogic
    {
        private readonly IBookingDao bookingDao;
        private readonly IResourceDao resourceDao; 
        
        public BookingLogic(IBookingDao bookingDao, IResourceDao resourceDao) 
        {
            this.bookingDao = bookingDao;
            this.resourceDao = resourceDao;
        } 
        
    public async Task<BookingResponseDto> BookResourceAsync(BookingCreationDto bookingToCreate)
    {
    ValidateData(bookingToCreate);
    
    var resource = await resourceDao.GetResourceByIdAsync(bookingToCreate.ResourceId);
    if (resource == null)
    {
        throw new Exception("Resource is not available.");
    }
    
    var existingBookings = (await bookingDao.GetBookingsByResourceIdAsync(bookingToCreate.ResourceId)).ToList();
    
    int bookedQuantityDuringPeriod = existingBookings
        .Where(b => !(b.DateFrom > bookingToCreate.DateTo || b.DateTo < bookingToCreate.DateFrom))
        .Sum(b => b.BookedQuantity);

    
    int availableQuantityDuringPeriod = resource.Quantity - bookedQuantityDuringPeriod;
    
    if (bookingToCreate.BookedQuantity > availableQuantityDuringPeriod)
    {
        throw new Exception("Requested quantity exceeds available quantity for the specified period.");
    }
    
    if (BookingHelper.IsConflict(existingBookings, bookingToCreate.DateFrom, bookingToCreate.DateTo, bookedQuantityDuringPeriod, resource.Quantity))
    {
        throw new Exception("Resource is already booked for the requested period.");
    }
    
    Booking toCreate = new Booking
    {
        ResourceId = bookingToCreate.ResourceId,
        BookedQuantity = bookingToCreate.BookedQuantity,
        DateFrom = bookingToCreate.DateFrom,
        DateTo = bookingToCreate.DateTo
    };

    Booking created = await bookingDao.BookResourceAsync(toCreate);

    return new BookingResponseDto
    {
        Success = true,
        Message = "Booking successful.",
        BookingId = created.Id
    };
}


        
    public static void ValidateData(BookingCreationDto bookingToCreate)
    {
            int resourceId = bookingToCreate.ResourceId;
            int bookedQuantity = bookingToCreate.BookedQuantity;
            DateOnly dateFrom = bookingToCreate.DateFrom;
            DateOnly dateTo = bookingToCreate.DateTo;
            
            if (resourceId <= 0)
            {
                throw new Exception("Resource ID must be greater than 0!");
            }
            
            if (bookedQuantity <= 0)
            {
                throw new Exception("Booked quantity must be greater than 0!");
            }
            
            if (dateFrom >= dateTo)
            {
                throw new Exception("Date from must be before date to!");
            }
        }
    }
}
