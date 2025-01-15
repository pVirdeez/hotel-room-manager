using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HotelManger.Utilities
{
    /// <summary>
    /// A custom JSON converter for date strings in the format 'yyyyMMdd'.
    /// </summary>
    public class DateConverter : JsonConverter<DateTime>
    {
        /// <summary>
        /// Reads a date string in the format 'yyyyMMdd' and converts it to a DateTime object.
        /// </summary>
        /// <param name="reader">The reader to get the date from</param>
        /// <param name="typeToConvert">The type to convert</param>
        /// <param name="options">The JSON serializer options</param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var dateString = reader.GetString();
            if (string.IsNullOrEmpty(dateString))
            {
                throw new JsonException("Date string cannot be null or empty");
            }

            if (DateTime.TryParseExact(dateString, "yyyyMMdd", null, DateTimeStyles.None, out DateTime result))
            {
                return result;
            }
            else
            {
                throw new JsonException($"Invalid date format. Expected 'yyyyMMdd', got '{dateString}'");
            }
        }

        /// <summary>
        /// Writes a DateTime object to a date string in the format 'yyyyMMdd'.
        /// </summary>
        /// <param name="writer">The writer to write the date to</param>
        /// <param name="value">The date to write</param>
        /// <param name="options">The JSON serializer options</param>
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyyMMdd"));
        }
    }
}