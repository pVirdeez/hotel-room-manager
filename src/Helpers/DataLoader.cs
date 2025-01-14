using System.Text.Json;
using HotelManger.Utilities;
/// <summary>
/// A helper class to load data files
/// </summary>
public static class DataLoader
{
  /// <summary>
  /// Load a JSON file and deserialise it into an object
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="filePath"></param>
  /// <returns></returns>
  /// <exception cref="FileNotFoundException"></exception>
  /// <exception cref="JsonException"></exception>
  public static T LoadJsonFile<T>(string filePath)
  {
    var options = new JsonSerializerOptions();
    options.Converters.Add(new DateConverter());

    if (!File.Exists(filePath))
    {
      throw new FileNotFoundException($"File not found: {filePath}");
    }
    var jsonData = File.ReadAllText(filePath);
    return JsonSerializer.Deserialize<T>(jsonData, options) ?? throw new JsonException($"Failed to deserilise JSON from {filePath}");
  }
}