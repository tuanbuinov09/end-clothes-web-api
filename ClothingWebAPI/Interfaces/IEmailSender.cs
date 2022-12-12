using ClothingWebAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingWebAPI.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
    }
}
