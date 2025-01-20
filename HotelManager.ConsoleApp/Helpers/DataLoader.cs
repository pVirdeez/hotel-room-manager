using System.Text.Json;
using HotelManager.Utilities;

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
    public static T LoadJsonFile<T>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"File not found: {filePath}");
        }

        // Add a custom date converter to handle date format 'yyyyMMdd'
        var options = new JsonSerializerOptions();
        options.Converters.Add(new DateConverter());

        // Read the JSON file and deserialize it
        var jsonData = File.ReadAllText(filePath);
        Console.WriteLine($"Loaded JSON: {jsonData}");
        var deserializedObject = JsonSerializer.Deserialize<T>(jsonData, options);
        if (deserializedObject == null)
        {
            throw new JsonException($"Failed to deserialize JSON from {filePath}");
        }
        return deserializedObject;
    }
}