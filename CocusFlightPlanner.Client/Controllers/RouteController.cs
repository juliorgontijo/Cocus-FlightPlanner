using AutoMapper;
using CocusFlightPlanner.Application.Services;
using CocusFlightPlanner.Application.ViewModel;
using CocusFlightPlanner.Common.Commands;
using CocusFlightPlanner.Common.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CocusFlightPlanner.Controllers
{
    public class RouteController : Controller
    {
        private readonly ILogger<RouteController> _logger;
        private readonly IRouteService _routeService;
        private readonly IMapper _mapper;
        private readonly IAirportService _airportService;
        public RouteController(ILogger<RouteController> logger, IRouteService routeService, IMapper mapper, IAirportService airportService)
        {
            _routeService = routeService;
            _logger = logger;
            _mapper = mapper;
            _airportService = airportService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {                
                var airCraftTypes = await _routeService.GetAll();
                var model = new RouteViewModel()
                {
                    Routes = airCraftTypes
                };
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(Guid? id)
        {
            try
            {
                RouteCommand command;
                if (id != null)
                {
                    var route = await _routeService.GetById(id.Value);
                    command = _mapper.Map<RouteCommand>(route);
                }
                else
                {
                    command = new RouteCommand();
                }

                var airports = await _airportService.GetAll();
                var model = new RouteUpsertViewModel()
                {
                    Airports = _mapper.Map<List<SelectListItem>>(airports),
                    Route = command
                };

                return View(model);              
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(RouteUpsertViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _routeService.Upsert(model.Route);
                    TempData["ToastMessage"] = "Record updated successfully!";
                }
                else
                {
                    var airports = await _airportService.GetAll();
                    model.Airports = _mapper.Map<List<SelectListItem>>(airports);

                    return View(model);
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(id);
                await _routeService.Delete(id.Value);
                TempData["ToastMessage"] = "Record deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetDistance(Guid originId, Guid destinationId)
        {
            try
            {
                var originAirport = await _airportService.GetById(originId);
                var destinationAirport = await _airportService.GetById(destinationId);

                if (originAirport == null || destinationAirport == null)
                    return Json(new { success = false });

                var distance = Util.CalculateDistanceInKilometers(originAirport, destinationAirport);

                return Json(new { success = true, distance });
            }
            catch
            {
                return Json(new { success = false });
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
