using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CocusFlightPlanner.Application.ViewModel
{
    public class RouteUpsertViewModel
    {
        public RouteCommand Route { get; set; }
        public List<SelectListItem>? Airports { get; set; }
    }
}
