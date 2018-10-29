using System;

namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            if(line == null)
            {
                logger.LogWarning("Line was null");
                return null;
            }
            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // Do not fail if one record parsing fails, return null
            // If your array.Length is less than 3, something went wrong
            if (cells.Length < 3 || cells.Length > 3)
            {
                // Log that and return null
                logger.LogWarning("The length was the array less than 3");
                return null;
            }
            TacoBell tacoBellLocation = new TacoBell();
            Point tacoBellLocationPoint = new Point();



            // grab the latitude from your array at index 0
            //validate the latitude if not log the error
            try
            {
                tacoBellLocationPoint.Latitude = Convert.ToDouble(cells[0]);
            }
            catch (Exception)
            {

                logger.LogError("Not a Number");
                return null;
            }
            if (tacoBellLocationPoint.Latitude < -90 || tacoBellLocationPoint.Latitude > 90)
            {
                logger.LogWarning("Not a Valid Latitude.");
                return null;
            }
            
            // grab the longitude from your array at index 1
            // validate the longitude if not log the error   
            try
            {
                tacoBellLocationPoint.Longitude = Convert.ToDouble(cells[1]);
            }
            catch (Exception)
            {

                logger.LogError("Not a Number");
                return null;
            }
            
            if (tacoBellLocationPoint.Longitude < -180 || tacoBellLocationPoint.Longitude > 180)
            {
                logger.LogWarning("Not a Valid Longitude");
                return null;
            }
            // grab the name from your array at index 2
            // validate the name if not log the error
            var name = cells[2].Trim();
            if (name.Length < 9 || name.Substring(0, 9) != "Taco Bell")
            {
                logger.LogWarning("Not a Valid location");
                return null;
            }



            tacoBellLocation.Name = name;
            tacoBellLocation.Location = tacoBellLocationPoint;
            
            
            return tacoBellLocation;
        }
    }
}