using System;

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
