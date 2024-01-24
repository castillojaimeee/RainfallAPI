using Sorted.Application.Interface;
using Sorted.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorted.Application.Service
{
    public class RainFallReadingService : IRainfallReadingService
    {
        private readonly IRainfallReadingRepository _rainfallReadingRepository;
        public RainFallReadingService(IRainfallReadingRepository rainfallReadingRepository)
        {
            this._rainfallReadingRepository = rainfallReadingRepository;
        }
        Task<List<RainfallReading>> IRainfallReadingService.GetRainfallReading(string stationId, int count)
        {
            return this._rainfallReadingRepository.GetRainfallReading(stationId, count);
        }
    }
}
