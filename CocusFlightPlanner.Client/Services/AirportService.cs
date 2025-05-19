using AutoMapper;
using CocusFlightPlanner.Application.Repositories;
using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using CocusFlightPlanner.Common.Models;

namespace CocusFlightPlanner.Application.Services
{
    public class AirportService: IAirportService
    {
        private readonly IRepository<Airport> _airportRepository;
        private readonly IMapper _mapper;

        public AirportService(IRepository<Airport> airportRepository, IMapper mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task<List<AirportDto>> GetAll()
        {
            var airports = await _airportRepository.GetAll(true);
            return _mapper.Map<List<AirportDto>>(airports);
        }

        public async Task<Airport> GetById(Guid id)
        {
            return await _airportRepository.GetById(true, id);
        }

        public async Task Upsert(AirportCommand airportDto)
        {
            Airport airport;
            if (airportDto.Id == null)
            {
                airport = new Airport();
                airport = _mapper.Map<Airport>(airportDto);
            }
            else
            {
                airport = await _airportRepository.GetById(false, airportDto.Id.Value);
                airport.Longitude = airportDto.Longitude;
                airport.Latitude = airportDto.Latitude;
                airport.Altitude = airportDto.Altitude;
                airport.IcaoCode = airportDto.IcaoCode;
                airport.IataCode = airportDto.IataCode;
            }
            await _airportRepository.Upsert(airport);
        }

        public async Task Delete(Guid id)
        {
            var airport = await _airportRepository.GetById(false, id);
            await _airportRepository.Delete(airport);
        }

    }   
}