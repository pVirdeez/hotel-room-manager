using HotelManager.Models;
using HotelManager.Services;

public partial class SearchAvailabilityTests
{
    private readonly List<Hotel> _testHotels;
    private readonly List<Booking> _testBookings;
    private readonly AvailabilityService _AvailabilityService;

    public SearchAvailabilityTests()
    {
        _testHotels = new List<Hotel> { TestDataFactory.CreateDefaultHotel() };
        _testBookings = new List<Booking>();
        _AvailabilityService = new AvailabilityService(_testHotels, _testBookings);
    }

    [Fact]
    public void Search_EmptyPeriod_ReturnsFullAvailability()
    {
        // Arrange
        var result = _AvailabilityService.SearchAvailabilty("H1", 7, "SGL");

        // Assert
        Assert.Single(result);
        Assert.Equal(2, result[0].AvailableRooms);
    }

    [Fact]
    public void Search_SingleBooking_CorrectlySegmentsPeriods()
    {
        // Arrange
        var baseDate = TestDataFactory.GetBaseDate();
        _testBookings.Add(TestDataFactory.CreateBooking());

        // Act
        var result = _AvailabilityService.SearchAvailabilty("H1", 7, "SGL");

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].AvailableRooms);
        Assert.Equal(2, result[1].AvailableRooms);
    }

    [Fact]
    public void Search_MultipleBookingsSameDay_CalculatesCorrectRoomCount()
    {
        // Arrange
        _testBookings.AddRange(TestDataFactory.CreateSameDayBookings());

        // Act
        var result = _AvailabilityService.SearchAvailabilty("H1", 7, "SGL");

        // Assert
        Assert.Single(result);
        Assert.Equal(2, result[0].AvailableRooms);
    }

    [Fact]
    public void Search_OverlappingBookings_CalculatesCorrectPeriods()
    {
        // Arrange
        _testBookings.AddRange(TestDataFactory.CreateOverlappingBookings());

        // Act
        var result = _AvailabilityService.SearchAvailabilty("H1", 7, "SGL");

        // Assert
        Assert.Equal(3, result.Count);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void Search_InvalidDaysAhead_ThrowsArgumentException(int daysAhead)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _AvailabilityService.SearchAvailabilty("H1", daysAhead, "SGL"));
    }

    [Fact]
    public void Search_InvalidHotelId_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _AvailabilityService.SearchAvailabilty("InvalidHotel", 7, "SGL"));
    }

    [Fact]
    public void Search_InvalidRoomType_ThrowsArgumentException()
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _AvailabilityService.SearchAvailabilty("H1", 7, "InvalidRoom"));
    }
}