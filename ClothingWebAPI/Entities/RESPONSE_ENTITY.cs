using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Entities
{
    public class RESPONSE_ENTITY
    {
        public RESPONSE_ENTITY(string errorDesc, string responseMessage)
        {
            this.errorDesc = errorDesc;
            this.responseMessage = responseMessage;
        }
        public RESPONSE_ENTITY()
        {
            
        }

        public string errorDesc { get; set; }
        public string responseMessage { get; set; }
    }
}
