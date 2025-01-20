using System.Text.Json;
using HotelManager.Models;
using HotelManager.Services;

namespace HotelManager.Commands
{
    public class SearchCommand
    {
        public static void Execute(string command, List<Hotel> hotels, List<Booking> bookings)
        {
            // extract the parameters from the input
            var parts = command.Substring("Search".Length).Trim('(', ')').Split(',');

            if (parts.Length != 3)
            {
                Console.WriteLine("Invalid format for Search command. Please use Search(H1, 35, SGL).");
                Console.WriteLine();
                return;
            }
            string hotelId = parts[0].Trim();
            int daysAhead = int.TryParse(parts[1].Trim(), out var days) ? days : 0;
            string roomType = parts[2].Trim();

            // call search availability function
            var availabilityService = new AvailabilityService(hotels, bookings);
            var roomSearchResult = availabilityService.SearchAvailabilty(hotelId, daysAhead, roomType);

            var formattedResults = roomSearchResult
            .Select(r => $"({r.StartDate:yyyyMMdd}-{r.EndDate:yyyyMMdd}, {r.AvailableRooms})")
            .ToList();

            Console.WriteLine(string.Join(", ", formattedResults));
        }
    }
}