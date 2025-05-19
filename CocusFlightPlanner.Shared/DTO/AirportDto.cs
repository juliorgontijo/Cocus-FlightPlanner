namespace CocusFlightPlanner.Common.DTO
{
    public class AirportDto
    {
        public Guid? Id { get; set; }

        public string Name { get; set; }
        public string IataCode { get; set; }
        public string IcaoCode { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Altitude { get; set; }
    }
}
