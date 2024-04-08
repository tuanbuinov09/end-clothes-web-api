using ClothingWebAPI.Entities;

namespace ClothingWebAPI.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
    }
}
