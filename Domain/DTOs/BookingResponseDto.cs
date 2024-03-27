namespace Shared.DTOs;

    public class BookingResponseDto
    {
        public bool Success { get; set; }
        
        public string Message { get; set; }
        
        public int? BookingId { get; set; }
    }