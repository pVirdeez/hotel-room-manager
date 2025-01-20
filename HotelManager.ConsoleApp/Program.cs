using HotelManager.Commands;
using HotelManager.Models;

class Program
{
    static async Task Main(string[] args)
    {
        // Extract command line arguments
        var hotelsFile = args.SkipWhile(a => a != "--hotels").Skip(1).FirstOrDefault();
        var bookingsFile = args.SkipWhile(a => a != "--bookings").Skip(1).FirstOrDefault();

        // Check the filenames were provided
        if (hotelsFile == null || bookingsFile == null)
        {
            Console.WriteLine("Please provide the paths to the hotels and bookings files using the --hotels and --bookings arguments.");
            return;
        }

        // Load the hotels and bookings files
        var hotels = await DataLoader.LoadJsonFileAsync<List<Hotel>>(hotelsFile);
        var bookings = await DataLoader.LoadJsonFileAsync<List<Booking>>(bookingsFile);
        Console.WriteLine("Hotel Manager Console App");

        while (true)
        {
            // Display console menu and prompt for input
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine(" Search(HotelId, Days, RoomType) - e.g. Search(H1, 35, SGL)");
            Console.WriteLine(" Availability(HotelId, Date, RoomType) - e.g. Availability(H1, 20240901, SGL)");
            Console.WriteLine(" Availability(HotelId, StartDate-EndDate, RoomType) - e.g. Availability(H1, 20240901-20240912, DBL)");
            Console.WriteLine();
            Console.WriteLine("Enter a command or press Enter to exit:");
            Console.Write("> ");
            var input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            // Execute the command
            CommandHandler.ExecuteCommand(input, hotels, bookings);
        }
    }
}