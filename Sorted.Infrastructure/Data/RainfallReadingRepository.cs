using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Sorted.Application.Interface;
using Sorted.Core.Entities;

namespace Sorted.Infrastructure.Data
{
    public class RainfallReadingRepository : IRainfallReadingRepository
    {
        public async Task<Response> GetRainfallReading(string stationId)
        {
            Response response = new Response();
            return response;
        }
    }
}
