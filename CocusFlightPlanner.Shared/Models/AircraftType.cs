using System.ComponentModel.DataAnnotations;

namespace CocusFlightPlanner.Common.Models
{
    public class AircraftType : EntityBase
    {
        [StringLength(50)]
        public string Name { get; set; }
        public int Mtow { get; set; }
        public float CruiseFuelBurn { get; set; }
    }
}
