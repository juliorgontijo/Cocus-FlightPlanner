using CocusFlightPlanner.Common.DTO;
using System.ComponentModel.DataAnnotations;

namespace CocusFlightPlanner.Common.Commands
{
    public class RouteCommand
    {
        public Guid? Id { get; set; }

        [Required]
        public Guid Origin { get; set; }

        [Required]
        public Guid Destination { get; set; }
    }
}
