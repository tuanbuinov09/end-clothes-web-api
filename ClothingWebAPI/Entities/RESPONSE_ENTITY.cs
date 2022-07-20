using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class RESPONSE_ENTITY
    {
        public RESPONSE_ENTITY()
        {
        }

        string errorDesc { get; set; }
        string responseMessage { get; set; }
    }
}
