using AutoMapper;
using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.DTO;
using CocusFlightPlanner.Common.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CocusFlightPlanner.Application.Startups
{
    public static class Mappers
    {
        public static void AddMappers(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AircraftType, AircraftTypeDto>();
                cfg.CreateMap<AircraftTypeCommand, AircraftType>()
                    .ForMember(dest => dest.Id, opt => opt.Ignore());
                cfg.CreateMap<AircraftType, AircraftTypeCommand>();

                cfg.CreateMap<Airport, AirportDto>();
                cfg.CreateMap<Airport, AirportCommand>();
                cfg.CreateMap<AirportCommand, Airport>();

                cfg.CreateMap<AirAirportDto, AirportDto>()
                    .ForMember(dest => dest.Altitude, opt => opt.MapFrom(src => src.Elevation.Value))
                    .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Geometry.Coordinates.First()))
                    .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Geometry.Coordinates.Skip(1).First()));
                cfg.CreateMap<AirportDto, SelectListItem>()
                    .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.IataCode));
                
                cfg.CreateMap<TravelRoute, RouteDto>();
                cfg.CreateMap<RouteCommand, TravelRoute>();
            });

            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
