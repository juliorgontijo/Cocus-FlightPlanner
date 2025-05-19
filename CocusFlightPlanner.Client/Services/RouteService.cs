using AutoMapper;
using CocusFlightPlanner.Application.Repositories;
using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using CocusFlightPlanner.Common.Helpers;
using CocusFlightPlanner.Common.Models;

namespace CocusFlightPlanner.Application.Services
{
    public class RouteService : IRouteService
    {
        private readonly IRepository<TravelRoute> _routeRepository;
        private readonly IRepository<AircraftType> _aircraftRepository;
        private readonly IRepository<Airport> _airporttRepository;
        private readonly IMapper _mapper;

        public RouteService(IRepository<TravelRoute> routeRepository, 
            IMapper mapper, 
            IRepository<AircraftType> aircraftRepository,
            IRepository<Airport> airporttRepository)
        {
            _routeRepository = routeRepository;
            _mapper = mapper;
            _aircraftRepository = aircraftRepository;
            _airporttRepository = airporttRepository;
        }

        public async Task<List<RouteDto>> GetAll()
        {
            var routes = await _routeRepository.GetAll(true,  x => x.Origin, x => x.Destination);
            var aircrafts = await _aircraftRepository.GetAll(true);

            var routesReturn = new List<RouteDto>();

            foreach (var route in routes.ToList())
            {
                foreach (var aircraft in aircrafts)
                {
                    var routeReturn = new RouteDto
                    {
                        Id = route.Id,
                        Origin = _mapper.Map<AirportDto>(route.Origin),
                        Destination = _mapper.Map<AirportDto>(route.Destination),
                        Aircraft = _mapper.Map<AircraftTypeDto>(aircraft),
                        Distance = route.Distance,
                        TotalFuelBurn = CalculateFuelBurn(route, aircraft)
                    };
                    routesReturn.Add(routeReturn);
                }
            }

            return routesReturn;
        }

        public async Task<TravelRoute> GetById(Guid id)
        {
            return await _routeRepository.GetById(true, id);
        }

        public async Task Upsert(RouteCommand route)
        {
            ArgumentNullException.ThrowIfNull(route);

            TravelRoute routeEntity;
            if (route.Id == null)
            {
                routeEntity = new TravelRoute();
            }
            else
            {
                routeEntity = await _routeRepository.GetById(false, route.Id.Value);
            }
            ArgumentNullException.ThrowIfNull(routeEntity);

            var origin = await _airporttRepository.GetById(false,route.Origin);
            var destination = await _airporttRepository.GetById(false,route.Destination);
            var distance = Util.CalculateDistanceInKilometers(origin, destination);

            routeEntity.Origin = origin;
            routeEntity.Destination = destination;
            routeEntity.Distance = distance;

            await _routeRepository.Upsert(routeEntity);
        }

        public async Task Delete(Guid id)
        {
            var route = await _routeRepository.GetById(false, id);
            await _routeRepository.Delete(route);
        }

        private double CalculateFuelBurn(TravelRoute route, AircraftType aircraft)
        {
            return route.Distance * aircraft.CruiseFuelBurn + (1 + route.Origin.Altitude/2500) * (1 + aircraft.Mtow / 250) * 200 * aircraft.CruiseFuelBurn;
        }
    }
}