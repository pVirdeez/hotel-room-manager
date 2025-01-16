using System.Text.Json;
using HotelManger.Models;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            var hotels = DataLoader.LoadJsonFile<List<Hotel>>("data/hotels.json");
            var bookings = DataLoader.LoadJsonFile<List<Booking>>("data/bookings.json");
            Console.WriteLine($"Hotel: {hotels.Count}");
            Console.WriteLine($"Bookings: {bookings.Count}");
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