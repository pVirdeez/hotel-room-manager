using System.Text.Json.Serialization;

namespace HotelManger.Models
{
  public class Booking
  {
    [JsonPropertyName("hotelId")]
    public required string HotelId { get; set; }
    [JsonPropertyName("arrival")]
    public required DateTime ArrivalDate { get; set; }
    [JsonPropertyName("departure")]
    public required DateTime DepartureDate { get; set; }
    [JsonPropertyName("roomType")]
    public required string RoomType { get; set; }
    [JsonPropertyName("roomRate")]
    public required string RoomRate { get; set; }

  }
}