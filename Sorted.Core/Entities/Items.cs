using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorted.Core.Entities
{
    public class Items
    {
        public string? Id { get; set; }
        public DateTime DateTime { get; set; }
        public string? Measure { get; set; }
        public float Value { get; set; }
    }
}
