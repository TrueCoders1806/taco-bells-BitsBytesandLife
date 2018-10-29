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

            logger.LogInfo($"Lines: {lines[0]}");

            var parser = new TacoParser();

            var locations = lines.Select(parser.Parse);

            // Create two `ITrackable` variables with initial values of `null`. These will be used to store your two taco bells that are the furthest from each other.
            // Create a `double` variable to store the distance
            ITrackable Itrack1 = null;
            ITrackable Itrack2 = null;
            double dist = 0;
            double longestDistance = 0;

            //First loop: loop over all of the all of the locations
            foreach (var location in locations)
            {
                //Grabbing a location from loop (locations)
                var locA = location.Location;
                //Creating a new GeoCoordinate with locA's latitude and longitude
                GeoCoordinate pin1 = new GeoCoordinate(locA.Latitude,locA.Longitude);

                //Second loop: loop on the locations with the scope of your first loop, 
                //so you can grab the "destination": locB
                foreach (var locationB in locations)
                {
                    //Grabbing a location from loop (locations)
                    var locB = locationB.Location;
                    //Creating a new GeoCoordinate with locB's latitude and longitude
                    GeoCoordinate pin2 = new GeoCoordinate(locB.Latitude,locB.Longitude);

                    // Now, compare the two using `.GetDistanceTo()`, which returns a double
                    // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
                    // double distanceBetween = pin1.GetDistanceTo(pin2);
                    dist = pin1.GetDistanceTo(pin2);
                    if (dist > longestDistance)
                    {
                        longestDistance = dist;
                        Itrack1 = location;
                        Itrack2 = locationB;
                    }
                }

            }
            
            //Convert the longestDistance using the formula; meters / 1609.344
            Console.WriteLine($"Starting Taco Bell:{Itrack1.Name} Ending Taco Bell:{Itrack2.Name} Longest Distance: {(longestDistance / 1609.344)} miles");
            Console.ReadLine();
        }
    }
}