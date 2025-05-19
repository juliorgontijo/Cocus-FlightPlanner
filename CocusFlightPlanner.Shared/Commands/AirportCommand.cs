using System.ComponentModel.DataAnnotations;

namespace CocusFlightPlanner.Common.Commands
{
    public class AirportCommand
    {
        public Guid? Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 5)]
        public string Name { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3)]
        public string IataCode { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string IcaoCode { get; set; }

        [Required]
        [Range(-90, 90)]
        public float Latitude { get; set; }

        [Required]
        [Range(-180, 180)]
        public float Longitude { get; set; }

        [Required]
        [Range(0, 5000)]
        public int Altitude { get; set; }
    }
}
