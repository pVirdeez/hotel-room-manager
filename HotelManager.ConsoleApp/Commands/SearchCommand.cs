using HotelManager.Models;
using HotelManager.Services;

namespace HotelManager.Commands
{
    /// <summary>
    /// Search command class to handle the search command
    /// </summary>
    public class SearchCommand
    {
        /// <summary>
        /// Execute the search command
        /// </summary>
        /// <param name="command">The command string to parse and execute.</param>
        /// <param name="hotels">The list of hotels.</param>
        /// <param name="bookings">The list of bookings.</param>
        /// output: result of the search command to console
        public static void Execute(string command, List<Hotel> hotels, List<Booking> bookings)
        {
            // extract the parameters from the input
            var parts = command.Substring("Search".Length).Trim('(', ')').Split(',');

            if (parts.Length != 3)
            {
                Console.WriteLine("Error: Invalid format for Search command. Please use Search(H1, 35, SGL).");
                return;
            }
            string hotelId = parts[0].Trim();
            int daysAhead = int.TryParse(parts[1].Trim(), out var days) ? days : 0;
            string roomType = parts[2].Trim();

            // call search availability function and display the result
            var availabilityService = new AvailabilityService(hotels, bookings);
            List<Availability> roomSearchResult;
            try
            {
                roomSearchResult = availabilityService.SearchAvailabilty(hotelId, daysAhead, roomType);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return;
            }

            var formattedResults = roomSearchResult
            .Select(r => $"({r.StartDate:yyyyMMdd}-{r.EndDate:yyyyMMdd}, {r.AvailableRooms})")
            .ToList();

            Console.WriteLine(string.Join(", ", formattedResults));
        }
    }
}