using CocusFlightPlanner.Common.Models;
using RT.Comb;

namespace CocusFlightPlanner.Common.Helpers
{
    public static class Util
    {
        public static readonly SqlCombProvider comb = new SqlCombProvider(new UnixDateTimeStrategy(), new UtcNoRepeatTimestampProvider().GetTimestamp);

        public static double CalculateDistanceInKilometers(Airport airport1, Airport airport2)
        {            
            const double EarthRadiusKm = 6371.0;

            if (airport1 == null || airport2 == null)
                return 0;

            double lat1Rad = DegreesToRadians(airport1.Latitude);
            double lat2Rad = DegreesToRadians(airport2.Latitude);
            double deltaLat = DegreesToRadians(airport2.Latitude - airport1.Latitude);
            double deltaLon = DegreesToRadians(airport2.Longitude - airport1.Longitude);

            double a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) +
                       Math.Cos(lat1Rad) * Math.Cos(lat2Rad) *
                       Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return EarthRadiusKm * c;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }
    }
}
