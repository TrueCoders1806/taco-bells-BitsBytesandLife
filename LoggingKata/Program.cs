using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            logger.LogInfo("Log initialized");

            var lines = File.ReadAllLines(csvPath);

           // logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse).ToArray();

            // Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the furthest from each other.
            // Create a `double` variable to store the distance
            ITrackable locA = null;
            ITrackable locB = null;
            double distance = 0;

            for (int i = 0; i < locations.Length; i++)
            {   
                // Creating a new Coordinate with locations locA lat and long
                var origin = new GeoCoordinate(locations[i].Location.Latitude, locations[i].Location.Longitude);

                for (int j = i + 1; j < locations.Length; j++)
                {
                    // Creating a new Coordinate with locations locB lat and long
                    var destination = new GeoCoordinate(locations[j].Location.Latitude, locations[j].Location.Longitude);

                    // double distanceBetween = pin1.GetDistanceTo(pin2);
                    var currentDistance = origin.GetDistanceTo(destination);

                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    // If the distance is greater than the currently saved distance, update 
                    // the distance and the two `ITrackable` (locA and locB) variables you set above
                    if (currentDistance > distance)
                    {
                        distance = currentDistance;
                        locA = locations[i];
                        locB = locations[j];
                    }
                }

            }

            logger.LogInfo("File Parsed");
            //Convert the longestDistance using the formula; meters / 1609.344
            Console.WriteLine($"Starting Taco Bell:{locA.Name}\nEnding Taco Bell:{locB.Name}\nLongest Distance: {(distance / 1609.344)} miles");
            Console.ReadLine();
        }
    }
}