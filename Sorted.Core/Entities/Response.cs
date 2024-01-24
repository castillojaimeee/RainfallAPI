using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorted.Core.Entities
{
    public class Response
    {
        public string? Context { get; set; }
        public Meta? Meta { get; set; }
        public List<Items>? Items { get; set; }
    }
}
