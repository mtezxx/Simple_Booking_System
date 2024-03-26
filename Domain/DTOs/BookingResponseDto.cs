namespace Shared.DTOs;

    public class BookingResponseDto
    {
        /// <summary>
        /// Indicates whether the booking operation was successful.
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Provides a message related to the booking operation. This could be a success message or an error message.
        /// </summary>
        public string Message { get; set; }
        
        /// <summary>
        /// The ID of the booking created, applicable if the booking was successful.
        /// </summary>
        public int? BookingId { get; set; }

        // Optional: Include additional fields as necessary, for example, details of the booking.
        // public DateTime DateFrom { get; set; }
        // public DateTime DateTo { get; set; }
        // public int BookedQuantity { get; set; }
    }