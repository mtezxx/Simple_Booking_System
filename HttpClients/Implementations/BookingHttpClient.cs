using System.Net.Http.Json;
using HttpClients.ClientInterfaces;
using Shared.DTOs;

namespace HttpClients.Implementations;

public class BookingHttpClient : IBookingService
{
    private readonly HttpClient client;
    
    public BookingHttpClient(HttpClient client)
    {
        this.client = client;
    }


    public async Task BookResourceAsync(BookingCreationDto bookingToCreate)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/bookings", bookingToCreate);

        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }
}