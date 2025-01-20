# Hotel Manager Application

This application is designed to manage hotel room availability, including the ability to search for room availability based on bookings and current inventory. It provides a console interface for querying the hotel data and booking information.

## Features

1. **Load data** - Load Hotel and Booking data from local fles on the file system.
2. **Check Availability** - Check the availability of a specific room type, within a hotel, for a given date range.

3. **Search Availability** - Search all availability for a specific room type, within a hotel, upto a given number of days ahead.

## Getting Started

### Cloning the Repository

```bash
# Clone this repository to your local machine
git clone https://github.com/pVirdeez/hotel-room-manager.git
cd HotelManager.ConsoleApp
```

### Building the Application

Run the following command to restore dependencies and build the project:

```bash
dotnet build
```

### Running the Application

To run the application with sample data, use the following command:

```bash
dotnet run --hotels data/hotels.json --bookings data/bookings.json
```

Test data has been included as part of the repository under the /data folder, to load your own data please amend the paths as required.

## Using the console menu

### Commands

The console menu provides two commands that a user can run to query the booking information:

1. **Check Availability**:

   ```bash
   Availability(<hotelId>,<startDate>,<roomTypeCode>)
   Availability(<hotelId>,<startDate>,<endDate>,<roomTypeCode>)
   ```

   Examples:

   ```bash
   Availability(H1, 20250122, SGL)
   Availability(H1, 20250122-20250210, SGL)
   ```

   Check availability of SGL rooms in hotel H1 on 2025-01-22 or between 2025-01-22 and 2025-02-10

2. **Search Availability**:
   ```bash
   Search(<hotelId>,<daysAhead>,<roomTypeCode>)
   ```
   Example:
   ```bash
   Search(H1,35,SGL)
   ```
   Search for SGL rooms, in hotel H1, 35 days ahead

### Input Data Format

Input data must follow the given JSON structure.

#### Hotel JSON data

```json
[
  {
    "id": "H1",
    "name": "Hotel California",
    "roomTypes": [
      {
        "code": "SGL",
        "description": "Single Room",
        "amenities": ["WiFi", "TV"],
        "features": ["Non-smoking"]
      },
      {
        "code": "DBL",
        "description": "Double Room",
        "amenities": ["WiFi", "TV", "Minibar"],
        "features": ["Non-smoking", "Sea View"]
      }
    ],

    "rooms": [
      {
        "roomType": "SGL",
        "roomId": "101"
      },
      {
        "roomType": "DBL",
        "roomId": "201"
      }
    ]
  }
]
```

#### Bookings JSON data

```json
[
  {
    "hotelId": "H1",
    "arrival": "20240901",
    "departure": "20240903",
    "roomType": "DBL",
    "roomRate": "Prepaid"
  },
  {
    "hotelId": "H1",
    "arrival": "20240902",
    "departure": "20240905",
    "roomType": "SGL",
    "roomRate": "Standard"
  }
]
```

## Unit Testing

This project uses `xUnit` for unit testing. The test project is located in the `HotelManager.Tests` folder.

### Running Tests

To run the tests, execute the following command:

```bash
cd HotelManager.Tests
dotnet test
```

## License

This project is licensed under the [MIT License](LICENSE).
