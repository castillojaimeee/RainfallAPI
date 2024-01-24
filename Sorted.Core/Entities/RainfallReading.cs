using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorted.Core.Entities
{
    public class RainfallReading
    {
        [JsonProperty(PropertyName = "DateTime")]
        public DateTime DateMeasured { get; set; }
        [JsonProperty(PropertyName = "Value")]
        public decimal? AmountMeasured { get; set; }

    }
}
