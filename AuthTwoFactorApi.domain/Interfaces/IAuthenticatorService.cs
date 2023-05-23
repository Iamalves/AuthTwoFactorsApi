using AuthTwoFactorApi.Domain.Models.Return;

namespace AuthTwoFactorApi.Domain.Interfaces
{
    public interface IAuthenticatorService
    {
        public TwoFactorGeneretorResponse GenerationTwoFactorValidation(string userMail);
        public bool ValidatorEntryCode(string userEnteredCode, string userMail);
    }
}
