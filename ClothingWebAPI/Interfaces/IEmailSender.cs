using ClothingWebAPI.Email;

namespace ClothingWebAPI.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(MailMessage message);
    }
}
