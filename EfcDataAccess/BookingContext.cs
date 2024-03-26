using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace EfcDataAccess;

public class BookingContext : DbContext
{
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Resource> Resources { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/Booking.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);            
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>().HasKey(b => b.Id);
        modelBuilder.Entity<Resource>().HasKey(r => r.Id);
        modelBuilder.Entity<Resource>().Property(r => r.Name).IsRequired();
        modelBuilder.Entity<Resource>().Property(r => r.Quantity).IsRequired();
        modelBuilder.Entity<Booking>().Property(b => b.BookedQuantity).IsRequired();
        modelBuilder.Entity<Booking>().Property(b => b.DateFrom).IsRequired();
        modelBuilder.Entity<Booking>().Property(b => b.DateTo).IsRequired();
        modelBuilder.Entity<Booking>().HasOne(b => b.Resource)
            .WithMany()
            .HasForeignKey(b => b.ResourceId); 
    }
}