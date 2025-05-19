namespace CocusFlightPlanner.Common.DTO
{
    public class AircraftTypeDto
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
        public int Mtow { get; set; }
        public float CruiseFuelBurn { get; set; }
    }
}
