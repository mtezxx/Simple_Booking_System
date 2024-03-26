using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Shared.DTOs;
using Shared.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Shared;

namespace Application.Logic
{
    public class BookingLogic : IBookingLogic
    {
        private readonly IBookingDao bookingDao;
        private readonly IResourceDao resourceDao; // Added for resource validation
        
        public BookingLogic(IBookingDao bookingDao, IResourceDao resourceDao) // Injecting IResourceDao
        {
            this.bookingDao = bookingDao;
            this.resourceDao = resourceDao;
        }
            
        public async Task<BookingResponseDto> BookResourceAsync(BookingCreationDto bookingToCreate)
        {
            ValidateData(bookingToCreate);
    
            // Check if resource exists and has enough available quantity
            var resource = await resourceDao.GetResourceByIdAsync(bookingToCreate.ResourceId);
            if (resource == null || resource.Quantity < bookingToCreate.BookedQuantity)
            {
                throw new Exception("Resource is not available or does not have enough quantity.");
            }
    
            // Check for booking conflicts
            var existingBookings = await bookingDao.GetBookingsByResourceIdAsync(bookingToCreate.ResourceId);
            // Assuming IsConflict is correctly defined to take IEnumerable<Booking>, DateOnly, and DateOnly as parameters
            if (BookingHelper.IsConflict(existingBookings, bookingToCreate.DateFrom, bookingToCreate.DateTo))
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
