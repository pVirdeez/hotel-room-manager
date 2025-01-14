using System.Text.Json.Serialization;

namespace HotelManger.Models
{
  public class Hotel
  {
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("roomTypes")]
    public List<RoomType>? RoomTypes { get; set; }
    [JsonPropertyName("rooms")]
    public List<Room>? Rooms { get; set; }
  }
}