using Application.DaoInterfaces;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Shared.Models;

namespace EfcDataAccess.DAOs;

public class BookingEfcDao : IBookingDao
{
    private readonly BookingContext context;
    
    public BookingEfcDao(BookingContext context)
    {
        this.context = context;
    }


    public Task<Booking> BookResourceAsync(Booking booking)
    {
        EntityEntry<Booking> newBooking = context.Bookings.Add(booking);
        context.SaveChanges();
        return Task.FromResult(newBooking.Entity);
    }

    public Task<IEnumerable<Booking>> GetBookingsByResourceIdAsync(int resourceId)
    {
        IQueryable<Booking> bookingsQuery = context.Bookings.AsQueryable();
        bookingsQuery = bookingsQuery.Where(b => b.ResourceId == resourceId);
        return Task.FromResult(bookingsQuery.AsEnumerable());
    }
}