using HotelManger.Models;

namespace HotelManger.Services
{
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

        /// <summary>
        /// Searches for available rooms in a hotel for a specified number of days ahead.
        /// </summary>
        /// <param name="hotelId">The id of the hotel to search.</param>
        /// <param name="daysAhead">The number of days ahead to search for available rooms.</param>
        /// <param name="roomTypeCode">Room type code to check</param>
        /// <returns></returns>
        public List<Availability> SearchAvailabilty(string hotelId, int daysAhead, string roomTypeCode)
        {
            var now = DateTime.Now;
            var endDate = now.AddDays(daysAhead);

            // Get initial state from CheckAvailability method
            var availableRooms = CheckAvailability(hotelId, now, now, roomTypeCode);

            // Get relevant bookings
            var bookingsToCheck = _bookings.Where(b =>
                b.HotelId == hotelId &&
                b.RoomType == roomTypeCode &&
                b.DepartureDate >= now &&
                b.ArrivalDate <= endDate)
                .ToList();

            // Accumulate changes by date (arrival and departure)
            var changesByDate = new Dictionary<DateTime, int>();
            foreach (var booking in bookingsToCheck)
            {
                if (booking.ArrivalDate >= now)
                    changesByDate[booking.ArrivalDate] = changesByDate.GetValueOrDefault(booking.ArrivalDate) - 1;
                if (booking.DepartureDate >= now)
                    changesByDate[booking.DepartureDate] = changesByDate.GetValueOrDefault(booking.DepartureDate) + 1;
            }

            // Convert to ordered list of changes
            var dateChanges = changesByDate
                .Select(d => new DateChange(d.Key, d.Value))
                .OrderBy(dc => dc.Date)
                .ToList();

            // Build availability periods
            var availablePeriods = new List<Availability>();
            var currentDate = now;

            foreach (var change in dateChanges.Where(d => d.Date <= endDate))
            {
                // If the date range has passed, record it as available
                if (currentDate < change.Date)
                {
                    availablePeriods.Add(new Availability
                    {
                        StartDate = currentDate,
                        EndDate = change.Date,
                        AvailableRooms = availableRooms
                    });
                }

                // Adjust available rooms based on arrivals and departures
                availableRooms += change.NetChange;
                currentDate = change.Date;
            }

            // If there's still time left after all bookings have been processed
            if (currentDate < endDate)
            {
                availablePeriods.Add(new Availability
                {
                    StartDate = currentDate,
                    EndDate = endDate,
                    AvailableRooms = availableRooms
                });
            }

            return availablePeriods;
        }
    }
}