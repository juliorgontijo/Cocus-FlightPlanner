using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using CocusFlightPlanner.Common.Models;

namespace CocusFlightPlanner.Application.Services
{
    public interface IRouteService
    {
        Task<List<RouteDto>> GetAll();
        Task<TravelRoute> GetById(Guid id);
        Task Upsert(RouteCommand airportDto);
        Task Delete(Guid id);
    }
}