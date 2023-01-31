using System;
using System.Threading.Tasks;
using Cw7.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripDbService _tripDbService;

        public TripController(ITripDbService tripDbService)
        {
            _tripDbService = tripDbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTrips()
        {
            try
            {
                var trips = await _tripDbService.GetTripsAsync();
                return Ok(trips);
            }
            catch (Exception e)
            {
                return NoContent();
            }
        }
    }
}