using System.Text.Json.Serialization;

namespace HotelManger.Models
{
  public class Room
  {
    [JsonPropertyName("roomType")]
    public required string RoomType { get; set; }
    [JsonPropertyName("roomId")]
    public required string RoomId { get; set; }
  }

  public class RoomType
  {
    [JsonPropertyName("code")]
    public required string Code { get; set; }
    [JsonPropertyName("description")]
    public required string Description { get; set; }
    [JsonPropertyName("amenities")]
    public List<string>? Amenities { get; set; }
    [JsonPropertyName("features")]
    public List<string>? Features { get; set; }
  }
}