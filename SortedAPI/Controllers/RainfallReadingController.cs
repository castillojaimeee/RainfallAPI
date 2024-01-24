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
        [Route("id/{stationId}/readings")]
        public ActionResult<RainfallReading> GetRainfallReading(string stationId, [FromQuery] QueryParam queryParam)
        {
            try
            {
                if (this.ModelState.IsValid)
                {
                    var result = this._rainfallReadingService.GetRainfallReading(stationId, queryParam.count).GetAwaiter().GetResult();
                    if (result.Count > 0)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return StatusCode(404, "No readings found for the specified stationId") ;
                    }
                }
                else
                {
                    return StatusCode(400, "Invalid request");
                }
               
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            } 
        }
    }
}
