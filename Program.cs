using HotelManger.Models;

class Program
{
  static void Main(string[] args)
  {
    var hotels = DataLoader.LoadJsonFile<List<Hotel>>("data/hotels.json");
    var bookings = DataLoader.LoadJsonFile<List<Booking>>("data/bookings.json");
    Console.WriteLine($"Hotel: {hotels.Count}");
    Console.WriteLine($"Bookings: {bookings.Count}");
  }
}