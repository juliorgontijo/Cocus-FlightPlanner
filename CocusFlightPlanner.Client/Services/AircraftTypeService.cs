using AutoMapper;
using CocusFlightPlanner.Application.Repositories;
using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using CocusFlightPlanner.Common.Models;

namespace CocusFlightPlanner.Application.Services
{
    public class AircraftTypeService : IAircraftTypeService
    {
        private readonly IRepository<AircraftType> _aircraftTypeRepository;
        private readonly IMapper _mapper;
        public AircraftTypeService(IRepository<AircraftType> aircraftTypeRepository, IMapper mapper)
        {
            _aircraftTypeRepository = aircraftTypeRepository;
            _mapper = mapper;
        }
        public async Task<List<AircraftTypeDto>> GetAll()
        {
            var aircrafts = await _aircraftTypeRepository.GetAll(true);
            return _mapper.Map<List<AircraftTypeDto>>(aircrafts);
        }
        public async Task<AircraftType> GetById(Guid id)
        {
            return await _aircraftTypeRepository.GetById(true, id);
        }

        public async Task Upsert(AircraftTypeCommand aircraftTypeDto)
        {
            AircraftType aircraftType;
            if (aircraftTypeDto.Id == null)
            {
                aircraftType = new AircraftType();
                aircraftType = _mapper.Map<AircraftType>(aircraftTypeDto);
            }
            else
            {
                aircraftType = await _aircraftTypeRepository.GetById(false, aircraftTypeDto.Id.Value);
                aircraftType.CruiseFuelBurn = aircraftTypeDto.CruiseFuelBurn;
                aircraftType.Mtow = aircraftTypeDto.Mtow;
                aircraftType.Name = aircraftTypeDto.Name;                
            }
            await _aircraftTypeRepository.Upsert(aircraftType);
        }

        public async Task Delete(Guid id)
        {
            var aircraftType = await _aircraftTypeRepository.GetById(false, id);
            await _aircraftTypeRepository.Delete(aircraftType);
        }

    }
}