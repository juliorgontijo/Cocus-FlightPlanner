namespace CocusFlightPlanner.Common.Models
{
    public class AircraftType : EntityBase
    {
        public string Name { get; set; }
        public int Mtow { get; set; }
        public float CruiseFuelBurn { get; set; }
    }
}
