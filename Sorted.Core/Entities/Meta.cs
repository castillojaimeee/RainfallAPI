using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorted.Core.Entities
{
    public class Meta
    {
        public string? Publisher { get; set; }
        public string? Licence { get; set; }
        public string? Documentation { get; set; }
        public string? Version { get; set; }
        public string? Comment { get; set; }
        public string[]? HasFormat { get; set; }
        public long Limit { get; set; }
    }
}
