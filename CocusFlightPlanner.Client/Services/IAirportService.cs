using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using CocusFlightPlanner.Common.Models;

namespace CocusFlightPlanner.Application.Services
{
    public interface IAirportService
    {
        Task<List<AirportDto>> GetAll();
        Task<Airport> GetById(Guid id);
        Task Upsert(AirportCommand airportDto);
        Task Delete(Guid id);

    }
}