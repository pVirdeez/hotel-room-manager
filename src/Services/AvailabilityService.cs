using HotelManger.Models;

/// <summary>
/// Service to check availability of rooms in a hotel
/// </summary>
public class AvailabilityService
{
    private readonly List<Hotel> _hotels;
    private readonly List<Booking> _bookings;

    public AvailabilityService(List<Hotel> hotels, List<Booking> bookings)
    {
        _hotels = hotels;
        _bookings = bookings;
    }

    /// <summary>
    /// Checks if rooms are available for a specified hotel, date range, and room type.
    /// </summary>
    /// <param name="hotelId">The unique identifier of the hotel to check.</param>
    /// <param name="arrivalDate">The start date of the requested booking period.</param>
    /// <param name="departureDate">The end date of the requested booking period.</param>
    /// <param name="roomTypeCode">The code identifying the desired room type.</param>
    /// <returns>The number of available rooms for the specified hotel, date range, and room type.</returns>
    /// <exception cref="ArgumentException">Thrown when input parameters are invalid, such as an improper date range.</exception>
    public int CheckAvailability(string hotelId, DateTime arrivalDate, DateTime departureDate, string roomTypeCode)
    {
        // Find the hotel by id
        var hotel = _hotels.FirstOrDefault(h => h.Id == hotelId);
        if (hotel == null)
        {
            throw new ArgumentException($"Hotel with id {hotelId} not found");
        }

        // Find the room type by code
        var roomType = hotel.RoomTypes?.FirstOrDefault(rt => rt.Code == roomTypeCode);
        if (roomType == null)
        {
            throw new ArgumentException($"Room type with code {roomTypeCode} not found in hotel {hotelId}");
        }

        // Filter bookings for the specified hotel and room type
        var bookings = _bookings.Where(b => b.HotelId == hotelId && b.RoomType == roomTypeCode);

        // Get the total number of rooms available for the specified room type
        var availableRoomsCount = hotel.Rooms?.Count(r => r.RoomType == roomTypeCode);
        if (availableRoomsCount == null)
        {
            throw new ArgumentException($"No rooms found in hotel {hotelId} with room type {roomTypeCode}");
        }

        // Check for overlapping bookings and reduce the available room count accordingly
        foreach (var booking in bookings)
        {
            if (arrivalDate < booking.DepartureDate && departureDate > booking.ArrivalDate)
            {
                availableRoomsCount--;
            }
        }

        return (int)availableRoomsCount;
    }
}