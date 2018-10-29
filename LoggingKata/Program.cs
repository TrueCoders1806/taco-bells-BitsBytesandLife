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
            double lognestDist = 0;

            // TODO:  Find the two Taco Bells in Alabama that are the furthest from one another.
            // HINT:  You'll need two nested forloops
            // Include the Geolocation toolbox, so you can compare locations: `using GeoCoordinatePortable;`
            // Do a loop for your locations to grab each location as the origin (perhaps: `locA`)
            // Create a new corA Coordinate with your locA's lat and long
            // Now, do another loop on the locations with the scope of your first loop, so you can grab the "destination" location (perhaps: `locB`)
            // Create a new Coordinate with your locB's lat and long
            // Now, compare the two using `.GetDistanceTo()`, which returns a double
            // If the distance is greater than the currently saved distance, update the distance and the two `ITrackable` variables you set above
            // double distanceBetween = pin1.GetDistanceTo(pin2);
            foreach (var location in locations)
            {
                var locA = location.Location;
                GeoCoordinate pin1 = new GeoCoordinate(locA.Latitude,locA.Longitude);
   
                foreach (var locationB in locations)
                {
                    var locB = locationB.Location; 
                    GeoCoordinate pin2 = new GeoCoordinate(locB.Latitude,locB.Longitude);
                    dist = pin1.GetDistanceTo(pin2);
                    
                    if (dist > lognestDist)
                    {
                        lognestDist = dist;
                        Itrack1 = location;
                        Itrack2 = locationB;
                    }
                }

            }

            Console.WriteLine($"Starting Taco Bell:{Itrack1.Name} Ending Taco Bell:{Itrack2.Name} Longest Distance:{lognestDist}");

            Console.ReadLine();
        }
    }
}