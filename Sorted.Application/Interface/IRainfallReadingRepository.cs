using Sorted.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorted.Application.Interface
{
    public interface IRainfallReadingRepository
    {
        Task<Response> GetRainfallReading(string stationId);
    }
}
