using System.ComponentModel.DataAnnotations;

namespace CocusFlightPlanner.Common.Commands
{
    public class AircraftTypeCommand
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Name { get; set; }

        [Required]
        [Range(0, 500)]
        public int Mtow { get; set; }

        [Required]
        [Range(0, 15)]
        public float CruiseFuelBurn { get; set; }
    }
}
