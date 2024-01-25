using Sorted.Application.Exceptions;
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
            var result = this._rainfallReadingRepository.GetRainfallReading(stationId, count);
            if (result.Result.Count > 0)
                return result;
            else
                throw new NotFoundException();
        }
    }
}
