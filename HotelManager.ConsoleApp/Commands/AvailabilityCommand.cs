using HotelManager.Models;
using HotelManager.Services;

namespace HotelManager.Commands
{
    public class AvailabilityCommand
    {
        public static void Execute(string command, List<Hotel> hotels, List<Booking> bookings)
        {
            // extract the parameters from the input
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
            if (datePart.Contains('-')) // date range
            {
                var dates = datePart.Split('-');
                if (dates.Length != 2 || !DateTime.TryParseExact(dates[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out startDate) ||
                    !DateTime.TryParseExact(dates[1], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out endDate))
                {
                    Console.WriteLine("Invalid date range format. Please use yyyyMMdd-yyyyMMdd.");
                    return;
                }
            }
            else // single date
            {
                if (!DateTime.TryParseExact(datePart, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out startDate))
                {
                    Console.WriteLine("Invalid date format. Please use yyyyMMdd.");
                    return;
                }
                endDate = startDate;
            }

            // call check availability function
            var availabilityService = new AvailabilityService(hotels, bookings);
            var roomCheckResult = availabilityService.CheckAvailability(hotelId, startDate, endDate, roomType);

            var dateMessage = startDate == endDate ? $"on {startDate:yyyy-MM-dd}" : $"from {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}";
            Console.WriteLine($"{roomCheckResult} {roomType} rooms available {dateMessage}");
        }
    }
}
