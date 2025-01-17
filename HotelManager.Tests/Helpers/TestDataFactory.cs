using HotelManger.Models;

public static class TestDataFactory
{
    private static DateTime _baseDate = DateTime.UtcNow.Date;
    public static DateTime GetBaseDate() => _baseDate;
    public static DateTime GetPastDate() => _baseDate.AddDays(-1);
    public static DateTime GetFutureDate(int daysAhead = 1) => _baseDate.AddDays(daysAhead);
    public static Hotel CreateDefaultHotel(string id = "H1") => new Hotel
    {
        Id = id,
        Name = $"Test Hotel {id}",
        RoomTypes = new List<RoomType>
        {
            new RoomType { Code = "SGL", Description = "Single Room" },
            new RoomType { Code = "DBL", Description = "Double Room" }
        },
        Rooms = new List<Room>
        {
            new Room { RoomId = "101", RoomType = "SGL" },
            new Room { RoomId = "102", RoomType = "SGL" },
            new Room { RoomId = "201", RoomType = "DBL" }
        }
    };

    public static Hotel CreateCustomHotel(
        string id,
        string name,
        Dictionary<string, int> roomTypeCounts)
    {
        var rooms = new List<Room>();
        var roomTypes = new List<RoomType>();

        foreach (var (typeCode, count) in roomTypeCounts)
        {
            roomTypes.Add(new RoomType { Code = typeCode, Description = $"{typeCode} Room" });
            for (int i = 1; i <= count; i++)
            {
                rooms.Add(new Room { RoomId = $"{typeCode}{i}", RoomType = typeCode });
            }
        }

        return new Hotel
        {
            Id = id,
            Name = name,
            RoomTypes = roomTypes,
            Rooms = rooms
        };
    }

    public static Booking CreateBooking(
        string hotelId = "H1",
        string roomType = "SGL",
        DateTime? arrivalDate = null,
        int stayDays = 1,
        string roomRate = "Standard")
    {
        arrivalDate ??= _baseDate;

        return new Booking
        {
            HotelId = hotelId,
            RoomType = roomType,
            ArrivalDate = arrivalDate.Value,
            DepartureDate = arrivalDate.Value.AddDays(stayDays),
            RoomRate = roomRate
        };
    }

    public static List<Booking> CreateOverlappingBookings(
        string hotelId = "H1",
        string roomType = "SGL")
    {
        return new List<Booking>
        {
            CreateBooking(hotelId, roomType, _baseDate, 2),
            CreateBooking(hotelId, roomType, _baseDate.AddDays(1), 2)
        };
    }

    public static List<Booking> CreateSameDayBookings(
        string hotelId = "H1",
        string roomType = "SGL",
        int count = 2)
    {
        return Enumerable.Range(0, count)
            .Select(_ => CreateBooking(hotelId, roomType, _baseDate, 1))
            .ToList();
    }

    public static void ResetBaseDate()
    {
        _baseDate = DateTime.UtcNow.Date;
    }
}