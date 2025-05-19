using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using CocusFlightPlanner.Common.Models;

namespace CocusFlightPlanner.Application.Services
{
    public interface IAircraftTypeService
    {
        Task<List<AircraftTypeDto>> GetAll();
        Task<AircraftType> GetById(Guid id);
        Task Upsert(AircraftTypeCommand aircraftTypeDto);
        Task Delete(Guid id);

    } 
}