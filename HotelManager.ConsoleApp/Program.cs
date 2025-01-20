using System.Text.Json;
using HotelManager.Commands;
using HotelManager.Helpers;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Parse arguments and load data
            var (hotelsFile, bookingsFile) = ArgumentParser.ParseArguments(args);
            var (hotels, bookings) = await DataLoader.LoadDataAsync(hotelsFile, bookingsFile);

            if (hotels.Count > 0 && bookings.Count > 0)
            {
                Console.WriteLine("Hotel Manager Console App");
                // Process user commands
                var commandProcessor = new CommandProcessor(hotels, bookings);
                commandProcessor.ProcessCommands();
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return;
        }
    }
}