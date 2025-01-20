using HotelManager.Models;

namespace HotelManager.Commands
{
    public class CommandHandler
    {
        public static void ExecuteCommand(string command, List<Hotel> hotels, List<Booking> bookings)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }

            switch (command.Split('(')[0].Trim().ToLower())
            {
                case "search":
                    SearchCommand.Execute(command, hotels, bookings);
                    break;

                case "availability":
                    AvailabilityCommand.Execute(command, hotels, bookings);
                    break;

                default:
                    Console.WriteLine("Unknown command. Please enter either 'Search' or 'Availability'.");
                    break;
            }
        }
    }
}