

namespace AuthTwoFactorApi.Domain.Interfaces
{
    public interface IMailSenderHandler
    {
        public void SendMail(string acessCode, string mail);
    }
}
