using AuthTwoFactorApi.Domain.Models;

namespace AuthTwoFactorApi.Domain.Interfaces
{
    public interface IAuthenticatorRepository
    {
        public void InsertUser(UserAuthCode model);
        public UserAuthCode GetKeyByEmail(string mail);
    }
}
