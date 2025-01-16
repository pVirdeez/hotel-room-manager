namespace HotelManger.Models
{
    public class Availability
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AvailableRooms { get; set; }
    }
    public record DateChange(DateTime Date, int NetChange);
}