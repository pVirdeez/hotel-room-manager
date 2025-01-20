using HotelManager.Commands;
using HotelManager.Models;

namespace HotelManager.Commands
{
    public class CommandProcessor
    {
        private readonly List<Hotel> _hotels;
        private readonly List<Booking> _bookings;

        public CommandProcessor(List<Hotel> hotels, List<Booking> bookings)
        {
            _hotels = hotels;
            _bookings = bookings;
        }

        public void ProcessCommands()
        {
            while (true)
            {
                DisplayMenu();
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) return;

                CommandHandler.ExecuteCommand(input, _hotels, _bookings);
            }
        }
        private void DisplayMenu()
        {
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("  - Search(HotelId, Days, RoomType) - e.g. Search(H1, 35, SGL)");
            Console.WriteLine("  - Availability(HotelId, Date, RoomType) - e.g. Availability(H1, 20240901, SGL)");
            Console.WriteLine("  - Availability(HotelId, StartDate-EndDate, RoomType) - e.g. Availability(H1, 20240901-20240912, DBL)");
            Console.WriteLine("\nEnter a command or press Enter to exit:");
            Console.Write("> ");
        }
    }
}