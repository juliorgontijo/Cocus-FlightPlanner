namespace CocusFlightPlanner.Common.Models
{
    public class Airport : EntityBase
    {
        public string Name { get; set; }
        public string IataCode { get; set; }
        public string IcaoCode { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Altitude { get; set; }
    }
}
