using AutoMapper;
using CocusFlightPlanner.Application.Services;
using CocusFlightPlanner.Application.ViewModel;
using CocusFlightPlanner.Common.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CocusFlightPlanner.Controllers
{
    public class AircraftController : Controller
    {
        private readonly ILogger<AircraftController> _logger;
        private readonly IAircraftTypeService _aircraftService;
        private readonly IMapper _mapper;

        public AircraftController(ILogger<AircraftController> logger, IAircraftTypeService aircraftService, IMapper mapper)
        {
            _aircraftService = aircraftService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var airCraftTypes = await _aircraftService.GetAll();
                var model = new AircraftTypeViewModel()
                {
                    AircraftTypes = airCraftTypes
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
                AircraftTypeCommand model;
                if (id != null)
                {
                    var aircraft = await _aircraftService.GetById(id.Value);
                    model = _mapper.Map<AircraftTypeCommand>(aircraft);
                }
                else
                {
                    model = new AircraftTypeCommand();
                }
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Upsert(AircraftTypeCommand aircraftType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _aircraftService.Upsert(aircraftType);
                    TempData["ToastMessage"] = "Record updated successfully!";
                }
                else
                {
                    return View(aircraftType);
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
                await _aircraftService.Delete(id.Value);
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
