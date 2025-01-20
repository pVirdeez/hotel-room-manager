using HotelManager.Models;
using HotelManager.Services;

public partial class CheckAvailabilityTests
{
    private readonly List<Hotel> _testHotels;
    private readonly List<Booking> _testBookings;
    private readonly AvailabilityService _availabilityService;

    public CheckAvailabilityTests()
    {
        _testHotels = new List<Hotel> { TestDataFactory.CreateDefaultHotel() };
        _testBookings = new List<Booking>();
        _availabilityService = new AvailabilityService(_testHotels, _testBookings);
    }

    [Fact]
    public void CheckAvailability_NoBookings_ReturnsAllRooms()
    {
        // Arrange
        var baseDate = TestDataFactory.GetBaseDate();

        // Act
        var availability = _availabilityService.CheckAvailability(
            "H1", baseDate, baseDate.AddDays(1), "SGL");

        // Assert
        Assert.Equal(2, availability);
    }

    [Fact]
    public void CheckAvailability_SingleBooking_ReturnsReducedCount()
    {
        // Arrange
        var baseDate = TestDataFactory.GetBaseDate();
        _testBookings.Add(TestDataFactory.CreateBooking());

        // Act
        var availability = _availabilityService.CheckAvailability(
            "H1", baseDate, baseDate.AddDays(1), "SGL");

        // Assert
        Assert.Equal(1, availability);
    }

    [Fact]
    public void CheckAvailability_FullyBooked_ReturnsZero()
    {
        // Arrange
        var baseDate = TestDataFactory.GetBaseDate();
        _testBookings.AddRange(TestDataFactory.CreateSameDayBookings());

        // Act
        var availability = _availabilityService.CheckAvailability(
            "H1", baseDate, baseDate.AddDays(1), "SGL");

        // Assert
        Assert.Equal(0, availability);
    }

    [Fact]
    public void CheckAvailability_PastDate_ThrowsArgumentException()
    {
        // Arrange
        var pastDate = TestDataFactory.GetBaseDate().AddDays(-1);

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _availabilityService.CheckAvailability(
                "H1", pastDate, pastDate.AddDays(1), "SGL"));
    }

    [Fact]
    public void CheckAvailability_InvalidDateRange_ThrowsArgumentException()
    {
        // Arrange
        var baseDate = TestDataFactory.GetBaseDate();

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
            _availabilityService.CheckAvailability(
                "H1", baseDate.AddDays(1), baseDate, "SGL"));
    }
}