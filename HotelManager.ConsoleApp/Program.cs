﻿using System.Text.Json;
using HotelManager.Commands;
using HotelManager.Models;

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            // Parse arguments and load data
            var (hotelsFile, bookingsFile) = ParseArguments(args);
            var (hotels, bookings) = await LoadDataAsync(hotelsFile, bookingsFile);

            if (hotels.Count > 0 && bookings.Count > 0)
            {
                // Process user commands
                Console.WriteLine("Hotel Manager Console App");
                ProcessCommands(hotels, bookings);
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
    /// <summary>
    /// Parse command line arguments to extract file paths.
    /// </summary>
    /// <param name="args">array of command line arguments</param>
    /// <returns>hotels file path and bookings file path</returns>
    /// <exception cref="ArgumentException"></exception>
    private static (string hotelsPath, string bookingsPath) ParseArguments(string[] args)
    {
        var hotelsFile = args.SkipWhile(a => a != "--hotels").Skip(1).FirstOrDefault();
        var bookingsFile = args.SkipWhile(a => a != "--bookings").Skip(1).FirstOrDefault();

        if (hotelsFile == null)
        {
            throw new ArgumentException("Missing hotels file path");
        }

        if (bookingsFile == null)
        {
            throw new ArgumentException("Missing bookings file path");
        }

        return (hotelsFile, bookingsFile);
    }

    /// <summary>
    /// Load hotels and bookings data from JSON files asynchronously.
    /// </summary>
    /// <param name="hotelsPath"></param>
    /// <param name="bookingsPath"></param>
    /// <returns></returns>
    private static async Task<(List<Hotel> hotels, List<Booking> bookings)> LoadDataAsync(
        string hotelsPath, string bookingsPath)
    {
        List<Hotel> hotels = new List<Hotel>();
        List<Booking> bookings = new List<Booking>();
        try
        {
            hotels = await DataLoader.LoadJsonFileAsync<List<Hotel>>(hotelsPath);
            bookings = await DataLoader.LoadJsonFileAsync<List<Booking>>(bookingsPath);
            return (hotels, bookings);
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return (new List<Hotel>(), new List<Booking>());
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return (new List<Hotel>(), new List<Booking>());
        }
    }

    /// <summary>
    /// Process user commands
    /// </summary>
    /// <param name="hotels">hotels data</param>
    /// <param name="bookings">bookings data</param>
    private static void ProcessCommands(List<Hotel> hotels, List<Booking> bookings)
    {
        while (true)
        {
            // Display console menu and prompt for input
            Console.WriteLine();
            Console.WriteLine("Commands:");
            Console.WriteLine("  Search(HotelId, Days, RoomType) - e.g. Search(H1, 35, SGL)");
            Console.WriteLine("  Availability(HotelId, Date, RoomType) - e.g. Availability(H1, 20240901, SGL)");
            Console.WriteLine("  Availability(HotelId, StartDate-EndDate, RoomType) - e.g. Availability(H1, 20240901-20240912, DBL)");
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