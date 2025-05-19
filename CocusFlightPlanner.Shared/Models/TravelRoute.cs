namespace CocusFlightPlanner.Common.Models
{
    public class TravelRoute : EntityBase
    {
        public Guid OriginId { get; set; }
        public Airport Origin { get; set; }
        public Guid DestinationId { get; set; }
        public Airport Destination { get; set; }
        public double Distance { get; set; }
    }
}
