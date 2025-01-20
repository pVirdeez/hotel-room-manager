namespace HotelManager.Utilities
{
    /// <summary>
    /// A helper class to parse command line arguments
    /// </summary>
    public static class ArgumentParser
    {
        /// <summary>
        /// Parse command line arguments to extract file paths.
        /// </summary>
        /// <param name="args">Array of command line arguments</param>
        /// <returns>Hotels file path and bookings file path</returns>
        /// <exception cref="ArgumentException"></exception>
        public static (string HotelsPath, string BookingsPath) ParseArguments(string[] args)
        {
            var hotelsFile = args.SkipWhile(a => a != "--hotels").Skip(1).FirstOrDefault();
            var bookingsFile = args.SkipWhile(a => a != "--bookings").Skip(1).FirstOrDefault();

            if (string.IsNullOrEmpty(hotelsFile))
                throw new ArgumentException("Missing hotels file path");

            if (string.IsNullOrEmpty(bookingsFile))
                throw new ArgumentException("Missing bookings file path");

            return (hotelsFile, bookingsFile);
        }
    }
}