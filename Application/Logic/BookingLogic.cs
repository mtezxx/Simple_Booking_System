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

            // Check if the resource exists
            var resource = await resourceDao.GetResourceByIdAsync(bookingToCreate.ResourceId);
            if (resource == null)
            {
                throw new Exception("Resource is not available.");
            }

            // Fetch existing bookings for the resource that overlap with the requested booking period
            var existingBookings = await bookingDao.GetBookingsByResourceIdAsync(bookingToCreate.ResourceId);
            int bookedQuantityDuringPeriod = existingBookings
                .Where(b => !(b.DateFrom > bookingToCreate.DateTo || b.DateTo < bookingToCreate.DateFrom))
                .Sum(b => b.BookedQuantity);

            // Calculate the available quantity during the requested period
            int availableQuantityDuringPeriod = resource.Quantity - bookedQuantityDuringPeriod;

            // If the requested quantity exceeds the available quantity, throw an exception
            if (bookingToCreate.BookedQuantity > availableQuantityDuringPeriod)
            {
                throw new Exception("Requested quantity exceeds available quantity for the specified period.");
            }

            // Check for booking conflicts (this seems redundant with the above checks, unless there are other conflict criteria)
            if (BookingHelper.IsConflict(existingBookings, bookingToCreate.DateFrom, bookingToCreate.DateTo))
            {
                throw new Exception("Resource is already booked for the requested period.");
            }

            // Proceed with creating the booking
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
