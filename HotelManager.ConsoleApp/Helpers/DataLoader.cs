using System.Text.Json;
using HotelManager.Models;
using HotelManager.Utilities;

namespace HotelManager.Helpers
{
    /// <summary>s
    /// A helper class to load data files
    /// </summary>
    public static class DataLoader
    {
        /// <summary>
        /// Loads a JSON file from the specified path and deserializes it into an object of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize into.</typeparam>
        /// <param name="filePath">The path to the JSON file.</param>
        /// <returns>The deserialized object of type T.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the specified file is not found.</exception>
        /// <exception cref="JsonException">Thrown if the file contents cannot be deserialized into the specified type.</exception>
        private static async Task<T> LoadJsonFileAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File not found: {filePath}.");
            }

            // Add a custom date converter to handle date format 'yyyyMMdd'
            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateConverter());

            // Read the JSON file and deserialize it
            var jsonData = await File.ReadAllTextAsync(filePath);

            var deserializedObject = JsonSerializer.Deserialize<T>(jsonData, options);
            if (deserializedObject == null)
            {
                throw new JsonException($"Failed to deserialize JSON from {filePath}.");
            }
            return deserializedObject;
        }

        /// <summary>
        /// Load hotels and bookings data from JSON files asynchronously.
        /// </summary>
        /// <param name="hotelsPath"></param>
        /// <param name="bookingsPath"></param>
        /// <returns>List of hotels and bookings</returns>
        public static async Task<(List<Hotel> hotels, List<Booking> bookings)> LoadDataAsync(
            string hotelsPath, string bookingsPath)
        {
            List<Hotel> hotels = new List<Hotel>();
            List<Booking> bookings = new List<Booking>();
            try
            {
                hotels = await LoadJsonFileAsync<List<Hotel>>(hotelsPath);
                bookings = await LoadJsonFileAsync<List<Booking>>(bookingsPath);
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
    }
}