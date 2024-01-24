using Microsoft.AspNetCore.Mvc;
using Sorted.Application.Interface;
using Sorted.Core.Entities;

namespace SortedAPI.Controllers
{
    [Route("rainfall")]
    [ApiController]
    public class RainfallReadingController : ControllerBase
    {
        private readonly IRainfallReadingService _rainfallReadingService;

        public RainfallReadingController(IRainfallReadingService rainfallReadingService)
        {
            this._rainfallReadingService = rainfallReadingService;
        }

        [HttpGet]
        [Route("id/{stationId}/readings")]
        public ActionResult<Response> GetRainfallReading(string stationId)
        {
            return Ok();
        }
    }
}
