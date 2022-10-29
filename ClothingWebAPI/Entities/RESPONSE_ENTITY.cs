using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class RESPONSE_ENTITY
    {
        public string affectedId { get; set; }
        public string errorDesc { get; set; }
        public string responseMessage { get; set; }
    
        public RESPONSE_ENTITY()
        {
            
        }

    }
}
