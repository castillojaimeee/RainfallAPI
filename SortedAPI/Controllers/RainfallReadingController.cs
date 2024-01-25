using Microsoft.AspNetCore.Mvc;
using Sorted.Application.Interface;
using Sorted.Core.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SortedAPI.Controllers
{
    [Route("rainfall")]
    [ApiController]
    public class RainfallReadingController : ControllerBase
    {
        public class QueryParam
        {
            [Range(1, 1000)]
            [DefaultValue(10)]
            public int count { get; set; }
        }

        private readonly IRainfallReadingService _rainfallReadingService;

        public RainfallReadingController(IRainfallReadingService rainfallReadingService)
        {
            this._rainfallReadingService = rainfallReadingService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RainfallReading>),StatusCodes.Status200OK)]
        [Route("id/{stationId}/readings")]
        public async Task<ActionResult<List<RainfallReading>>> GetRainfallReading(string stationId, [FromQuery] QueryParam queryParam)
        {
            return Ok(await this._rainfallReadingService.GetRainfallReading(stationId, queryParam.count));
        }
    }
}
