using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class REPORT_REQUEST_ENTITY
    {
        public string method { get; set; }
        public string responseType { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
