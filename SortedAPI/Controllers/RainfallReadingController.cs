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
        [ProducesResponseType(typeof(RainfallReading),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        [Route("id/{stationId}/readings")]
        public ActionResult<RainfallReading> GetRainfallReading(string stationId, [FromQuery] QueryParam queryParam)
        {
            try
            {
                var result = this._rainfallReadingService.GetRainfallReading(stationId, queryParam.count).GetAwaiter().GetResult();
                if (result.Count > 0)
                {
                    return Ok(result);
                }
                else
                {
                    return StatusCode(404, new JsonResult(new ErrorResponse { Message = "No readings found for the specified stationId" }));
                }
            }
            catch
            {
                return StatusCode(500, new JsonResult(new ErrorResponse { Message = "Internal server error" }));
            } 
        }
    }
}
