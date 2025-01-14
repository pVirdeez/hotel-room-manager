using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelManger.Utilities
{
  public class DateConverter : JsonConverter<DateTime>
  {
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
      var dateString = reader.GetString();
      if (string.IsNullOrEmpty(dateString))
      {
        throw new JsonException("Date string cannot be null or empty");
      }

      try
      {
        return DateTime.ParseExact(dateString, "yyyyMMdd", null);
      }
      catch (FormatException ex)
      {
        throw new JsonException($"Invalid date format. Expected 'yyyyMMdd', got '{dateString}'", ex);
      }
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
      writer.WriteStringValue(value.ToString("yyyyMMdd"));
    }
  }
}