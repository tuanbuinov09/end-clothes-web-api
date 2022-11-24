using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Interfaces
{
    public interface IJwtAuthenticationManager
    {
       public string authenticate(string email, string password)
        {
            throw new NotImplementedException();
        }
        
    }
}
