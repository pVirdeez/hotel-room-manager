using System.Text.Json;
using HotelManger.Models;
using HotelManger.Services;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var hotels = DataLoader.LoadJsonFile<List<Hotel>>("data/hotels.json");
            var bookings = DataLoader.LoadJsonFile<List<Booking>>("data/bookings.json");

            var availabilityService = new AvailabilityService(hotels, bookings);
            var roomCheckResult = availabilityService.CheckAvailability("H1", new DateTime(2024, 09, 03), DateTime.Now, "SGL");
            Console.WriteLine($"Availability: {roomCheckResult}");

            var roomSearchResult = availabilityService.SearchAvailabilty("H1", 7, "SGL");
            Console.WriteLine($"Available rooms: {JsonSerializer.Serialize(roomSearchResult, new JsonSerializerOptions { WriteIndented = true })}");

        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Please ensure the required files are in place.");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error: {ex.Message}. Please check the file format.");
        }
    }
}