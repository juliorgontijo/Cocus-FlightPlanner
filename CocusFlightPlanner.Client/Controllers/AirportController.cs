using AutoMapper;
using CocusFlightPlanner.Application.Services;
using CocusFlightPlanner.Application.ViewModel;
using CocusFlightPlanner.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CocusFlightPlanner.Controllers
{
    public class AirportController : Controller
    {
        private readonly ILogger<AirportController> _logger;
        private readonly IAirportService _airportService;
        private readonly IMapper _mapper;
        private readonly OpenAipService _service;
        public AirportController(ILogger<AirportController> logger, IAirportService airportService, IMapper mapper, OpenAipService service)
        {
            _airportService = airportService;
            _logger = logger;
            _mapper = mapper;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            try
            {                
                var airCraftTypes = await _airportService.GetAll();
                var model = new AirportViewModel()
                {
                    Airports = airCraftTypes
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
                AirportCommand model;
                if (id != null)
                {
                    var airport = await _airportService.GetById(id.Value);
                    model = _mapper.Map<AirportCommand>(airport);
                }
                else
                {
                    model = new AirportCommand();
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }


        [HttpGet]
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _airportService.GetById(id);
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<JsonResult> GetData(string iataCode, string icaoCode)
        {
            try
            {
                var result = await _service.GetAirportsAsync(iataCode, icaoCode);
                return new JsonResult(result);
            }
            catch (Exception)
            {
                return new JsonResult(null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(AirportCommand airport)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _airportService.Upsert(airport);
                    TempData["ToastMessage"] = "Record updated successfully!";
                }
                else
                {
                    return View(airport);
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
                await _airportService.Delete(id.Value);
                TempData["ToastMessage"] = "Record deleted successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }    
}
