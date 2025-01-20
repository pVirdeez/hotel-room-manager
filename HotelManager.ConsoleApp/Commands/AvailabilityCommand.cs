using HotelManager.Models;
using HotelManager.Services;

namespace HotelManager.Commands
{
    /// <summary>
    /// Availability command class to handle the availability command
    /// </summary>
    public class AvailabilityCommand
    {
        /// <summary>
        /// Execute the availability command
        /// </summary>
        /// <param name="command">The command string to parse and execute</param>
        /// <param name="hotels">The list of hotels</param>
        /// <param name="bookings">The list of bookings</param>
        /// output: result of the availability command to console
        public static void Execute(string command, List<Hotel> hotels, List<Booking> bookings)
        {
            // Extract the parameters from the input
            var parts = command.Substring("Availability".Length).Trim('(', ')').Split(',');

            if (parts.Length != 3)
            {
                Console.WriteLine("Invalid format for Availability command. Please use Availability(H1, 20240901, SGL) or Availability(H1, 20240901-20240912, SGL).");
                return;
            }
            string hotelId = parts[0].Trim();
            string datePart = parts[1].Trim();
            string roomType = parts[2].Trim();
            DateTime startDate, endDate;

            // Parse the date(s)
            if (datePart.Contains('-'))
            {
                var dates = datePart.Split('-');
                if (dates.Length != 2 || !DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out startDate) ||
                    !DateTime.TryParseExact(dates[1], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out endDate))
                {
                    Console.WriteLine("Error: Invalid date range format. Please use yyyyMMdd-yyyyMMdd.");
                    return;
                }
            }
            else
            {
                if (!DateTime.TryParseExact(datePart, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out startDate))
                {
                    Console.WriteLine("Error: Invalid date format. Please use yyyyMMdd.");
                    return;
                }
                endDate = startDate;
            }

            // Call check availability function and display the result
            var availabilityService = new AvailabilityService(hotels, bookings);
            int roomCheckResult;
            try
            {
                roomCheckResult = availabilityService.CheckAvailability(hotelId, startDate, endDate, roomType);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Error: {e.Message}");
                return;
            }

            var dateMessage = startDate == endDate ? $"on {startDate:yyyy-MM-dd}" : $"from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}";
            Console.WriteLine($"{roomCheckResult} {roomType} rooms available {dateMessage}.");
        }
    }
}
