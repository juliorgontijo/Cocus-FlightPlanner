using System.ComponentModel.DataAnnotations;

namespace CocusFlightPlanner.Common.Models
{
    public class Airport : EntityBase
    {
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(3)]
        public string IataCode { get; set; }

        [StringLength(4)]
        public string IcaoCode { get; set; }

        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public int Altitude { get; set; }
    }
}
