namespace CocusFlightPlanner.Common.DTO
{
    public class AipAirportListDto
    {
        public List<AirAirportDto>? items { get; set; }
    }

    public class AirAirportDto
    {
        public string Name { get; set; }
        public string IataCode { get; set; }
        public string IcaoCode { get; set; }
        public Elevation Elevation { get; set; }
        public Geometry Geometry { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Altitude { get; set; }
    }

    public class Elevation
    {
        public int Value { get; set; }
    }

    public class Geometry
    {
        public float[] Coordinates { get; set; }
    }
    
}
