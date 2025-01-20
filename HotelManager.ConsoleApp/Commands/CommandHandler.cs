using HotelManager.Models;

namespace HotelManager.Commands
{
    /// <summary>
    /// Command handler class to manage the command execution
    /// </summary>
    public class CommandHandler
    {
        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="command">The command string to execute</param>
        /// <param name="hotels">The list of hotels</param>
        /// <param name="bookings">The list of bookings</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <summery>
        public static void ExecuteCommand(string command, List<Hotel> hotels, List<Booking> bookings)
        {
            if (string.IsNullOrEmpty(command))
            {
                throw new ArgumentNullException(nameof(command));
            }

            // check the command and execute the appropriate command
            switch (command.Split('(')[0].Trim().ToLower())
            {
                case "search":
                    SearchCommand.Execute(command, hotels, bookings);
                    break;

                case "availability":
                    AvailabilityCommand.Execute(command, hotels, bookings);
                    break;

                default:
                    Console.WriteLine("Error: Unknown command. Please enter either 'Search' or 'Availability'.");
                    break;
            }
        }
    }
}