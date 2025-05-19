using CocusFlightPlanner.Common.Helpers;
using System.ComponentModel.DataAnnotations;

namespace CocusFlightPlanner.Common.Models
{
    public class EntityBase
    {
        [Key]
        [MaxLength(32)]
        public Guid Id { get; set; }

        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool Deleted { get; set; }


        protected EntityBase()
        {
            // RTComb is a library used to create sequential GUIDs. It helps preventing SQL table index fragmentation over time.
            Id = Util.comb.Create();
            CreatedAt = DateTimeOffset.UtcNow;
        }
    }
}
