namespace CocusFlightPlanner.Common.DTO
{
    public class RouteDto
    {
        public Guid? Id { get; set; }
        public AirportDto Origin { get; set; }
        public AirportDto Destination { get; set; }
        public double Distance { get; set; }
        public AircraftTypeDto Aircraft { get; set; }
        public double TotalFuelBurn { get; set; }
    }
}
